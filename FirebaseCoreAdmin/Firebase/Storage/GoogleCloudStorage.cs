namespace FirebaseCoreAdmin.Firebase.Storage
{
    using FirebaseCoreAdmin.Configurations.ServiceAccounts;
    using FirebaseCoreAdmin.Extensions;
    using FirebaseCoreAdmin.HttpClients;
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;
    using System.Net;
    using FirebaseCoreAdmin.Configurations;
    using FirebaseCoreAdmin.Firebase.Auth;
    using System.Threading.Tasks;
    using System.Net.Http;

    public class GoogleCloudStorage : IGoogleStorage, IDisposable
    {
        private IFirebaseHttpClient _httpClient;
        private IServiceAccountCredentials _credentials;
        private IFirebaseConfiguration _firebaseConfiguration;

        public GoogleCloudStorage(IFirebaseAdminAuth auth, IServiceAccountCredentials credentials)
        {
            var firebaseConfiguration = new DefaultFirebaseConfiguration(GoogleServiceAccess.StorageOnly);
            var storageAuthority = new Uri($"{firebaseConfiguration.StorageBaseAuthority.TrimSlashes()}", UriKind.Absolute);

            _httpClient = new FirebaseHttpClient(credentials, firebaseConfiguration, storageAuthority);
            _credentials = credentials;
            _firebaseConfiguration = firebaseConfiguration;

            auth.AddFirebaseHttpClient(_httpClient);
        }

        public string GetPublicUrl(string path)
        {
            if (String.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            string normalziedPath = WebUtility.UrlEncode(path.TrimSlashes());

            var auth = _httpClient.GetAuthority().ToString().TrimSlashes();
            var fullPath = new Uri($"{auth}/{_credentials.GetDefaultBucket()}/{normalziedPath}?alt=media", UriKind.Absolute);
            return fullPath.AbsoluteUri;
        }

        public string GetSignedUrl(SigningOption options)
        {
            ValidateOptions(options);

            var signingPayloadAsString = string.Join("\n", BuildSigningPayload(options));
            var encryptedBase64String = EncryptPayload(signingPayloadAsString);

            return PrepareSignedUrl(options, encryptedBase64String);
        }

        public async Task RemoveObjectAsync(string path)
        {
            string urlEncodedPath = GetUrlEncodedPath(path);
            var deleteUri = new Uri($"{ _firebaseConfiguration.StorageBaseAuthority2}/v1/b/{_credentials.GetDefaultBucket()}/o/{urlEncodedPath}", UriKind.Absolute);
            await _httpClient.SendStorageRequestAsync<object>(deleteUri, HttpMethod.Delete);

            return;
        }

        public async Task<(bool Result, Exception ex)> TryRemoveObjectAsync(string path)
        {
            try
            {
                await RemoveObjectAsync(path);
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }


        public async Task MoveObjectAsync(string originPath, string destinationPath)
        {
            string urlEncodedOrigin = GetUrlEncodedPath(originPath);
            string urlEncodedDestination = GetUrlEncodedPath(destinationPath);

            var reWriteUri = new Uri($"{ _firebaseConfiguration.StorageBaseAuthority2}/v1/b/{_credentials.GetDefaultBucket()}/o/{urlEncodedOrigin}/rewriteTo/b/{_credentials.GetDefaultBucket()}/o/{urlEncodedDestination}", UriKind.Absolute);
            await _httpClient.SendStorageRequestAsync<object>(reWriteUri, HttpMethod.Post);

            await RemoveObjectAsync(originPath);

            return;
        }

        public async Task<(bool Result, Exception ex)> TryMoveObjectAsync(string originPat, string destinationPath)
        {
            try
            {
                await MoveObjectAsync(originPat, destinationPath);
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }

        public async Task<ObjectMetadata> GetObjectMetaDataAsync(string path)
        {
            string urlEncodedPath = GetUrlEncodedPath(path);
            var metaUri = new Uri($"{ _firebaseConfiguration.StorageBaseAuthority2}/v1/b/{_credentials.GetDefaultBucket()}/o/{urlEncodedPath}", UriKind.Absolute);
            return await _httpClient.SendStorageRequestAsync<ObjectMetadata>(metaUri, HttpMethod.Get);
        }

        private string GetUrlEncodedPath(string path)
        {
            string normalizedPath = path.TrimSlashes();

            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            return WebUtility.UrlEncode(normalizedPath);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _httpClient.Dispose();
            }
        }

        ~GoogleCloudStorage()
        {
            Dispose(false);
        }

        private string PrepareSignedUrl(SigningOption options, string signature)
        {
            var uri = new UriBuilder();
            uri.Scheme = "https";
            uri.Host = $"{_firebaseConfiguration.StorageBaseAuthority.TrimSlashes().Replace("https://", "")}/{_credentials.GetDefaultBucket().TrimSlashes()}";
            uri.Path = options.Path.Trim().TrimSlashes();
            uri.Query = string.Format("GoogleAccessId={0}&Expires={1}&Signature={2}", _credentials.GetServiceAccountEmail(),
                                                                                      BuildExpiration(options.ExpireDate), WebUtility.UrlEncode(signature));
            return uri.Uri.AbsoluteUri;
        }

        private string EncryptPayload(string signingPayloadAsString)
        {
            var encryptedBase64String = String.Empty;
            UTF8Encoding byteConverter = new UTF8Encoding();
            var payloadBytes = byteConverter.GetBytes(signingPayloadAsString);

            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(_credentials.GetRSAParams());
                var encrypt = rsa.SignData(payloadBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                encryptedBase64String = Convert.ToBase64String(encrypt);
            }
            return encryptedBase64String;
        }

        private IList<string> BuildSigningPayload(SigningOption options)
        {
            var payload = new List<string>()
            {
                BuildActionMethod(options.Action),
                BuildContentMD5(options.ContentMD5),
                BuildContentType(options.ContentType),
                BuildExpiration(options.ExpireDate),
                BuilCanonicalizedResource(options.Path)
            };

            return payload;
        }

        private string BuildActionMethod(SigningAction action)
        {
            string actionMethod = string.Empty;
            switch (action)
            {
                case SigningAction.Read:
                    actionMethod = "GET";
                    break;
                case SigningAction.Write:
                    actionMethod = "PUT";
                    break;
                case SigningAction.Delete:
                    actionMethod = "DELETE";
                    break;
                default:
                    break;
            }

            return actionMethod;
        }

        private string BuildContentMD5(string contentMD5)
        {
            if (String.IsNullOrWhiteSpace(contentMD5))
            {
                return string.Empty;
            }
            return contentMD5.Trim();
        }

        private string BuildContentType(string contentType)
        {
            if (string.IsNullOrWhiteSpace(contentType))
            {
                return String.Empty;
            }
            return contentType.Trim();
        }

        private string BuildExpiration(DateTime dateTo)
        {
            return dateTo.ToUnixSeconds().ToString();
        }

        private string BuilCanonicalizedResource(string path)
        {
            return $"/{_credentials.GetDefaultBucket().Trim().TrimSlashes()}/{WebUtility.UrlEncode(path.TrimSlashes())}";
        }

        private void ValidateOptions(SigningOption options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            if (options.ExpireDate.ToUnixSeconds() == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(options.ExpireDate), "ExpireDate should be reasonable value");
            }
            if (String.IsNullOrWhiteSpace(options.Path))
            {
                throw new ArgumentNullException(nameof(options.Path));
            }
        }
    }
}

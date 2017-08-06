
namespace FirebaseCoreAdmin.Firebase.Database
{
    using FirebaseCoreAdmin.Extensions;
    using FirebaseCoreAdmin.HttpClients;
    using System;
    using System.Collections.Generic;

    public class FirebaseAdminRef : IFirebaseAdminRef
    {
        private readonly string _basePath;
        private readonly IFirebaseHttpClient _httpClient;
        private readonly List<KeyValuePair<string, string>> _queryStore = new List<KeyValuePair<string, string>>();

        public IFirebaseHttpClient HttpClient => _httpClient;

        public string Path => _basePath;

        public FirebaseAdminRef(IFirebaseHttpClient httpClient, string refPath)
        {
            if (String.IsNullOrWhiteSpace(refPath))
            {
                throw new ArgumentNullException(nameof(refPath));
            }
            var normalizedPath = $"{refPath.TrimSlashes()}.json";
            _httpClient = httpClient;
            _basePath = normalizedPath;
        }
        public void Add(string key, string value)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            _queryStore.Add(new KeyValuePair<string, string>(key, value));
        }

        public IList<KeyValuePair<string, string>> GetQueryStore()
        {
            return _queryStore;
        }
    }
}

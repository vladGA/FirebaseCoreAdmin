namespace FirebaseCoreAdmin.Configurations.ServiceAccounts
{
    using System.Security.Cryptography.X509Certificates;
    using System.Security.Cryptography;

    public class P12ServiceAccountCredentials : IServiceAccountCredentials
    {
        private readonly string _fileName;
        private readonly string _password;
        private readonly string _serviceAccountEmail;
        private readonly string _projectId;

        public P12ServiceAccountCredentials(string fileName, string password, string serviceAccountEmail, string projectId)
        {
            _serviceAccountEmail = serviceAccountEmail;
            _fileName = fileName;
            _password = password;
            _projectId = projectId;
        }

        public string GetDefaultBucket()
        {
            return $"{_projectId}.appspot.com";
        }

        public string GetProjectId()
        {
            return _projectId;
        }

        public RSAParameters GetRSAParams()
        {
            using (var cert = new X509Certificate2(_fileName, _password))
            {
                return cert.GetRSAPrivateKey().ExportParameters(true);
            }
        }

        public string GetServiceAccountEmail()
        {
            return _serviceAccountEmail;
        }
    }
}

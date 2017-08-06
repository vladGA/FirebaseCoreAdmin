namespace FirebaseCoreAdmin.Configurations.ServiceAccounts
{
    using FirebaseCoreAdmin.Extensions;
    using Newtonsoft.Json;
    using Org.BouncyCastle.Crypto.Parameters;
    using Org.BouncyCastle.OpenSsl;
    using System.IO;
    using System.Security.Cryptography;

    public class JSONServiceAccountCredentials : IServiceAccountCredentials
    {
        private JsonServiceAccountModel _jsonModel;

        private RSAParameters _rsaParam;

        public JSONServiceAccountCredentials(string fileName)
        {
            InitializeFromFile(fileName);
        }

        public JSONServiceAccountCredentials(string content, bool fromFile)
        {
            if (fromFile)
                InitializeFromFile(content);
            else InitializeFromString(content);
        }

        public virtual string GetProjectId()
        {
            return _jsonModel.ProjectId;
        }

        public virtual string GetDefaultBucket()
        {
            return $"{_jsonModel.ProjectId}.appspot.com";
        }

        public RSAParameters GetRSAParams()
        {
            return _rsaParam;
        }

        public virtual string GetServiceAccountEmail()
        {
            return _jsonModel.ClientEmail;
        }

        private void InitializeFromFile(string fileName)
        {
            _jsonModel = JsonConvert.DeserializeObject<JsonServiceAccountModel>(File.ReadAllText(fileName));

            if (_jsonModel == null)
            {
                throw new FileLoadException("Incorrect json file was provided");
            }
            FillRsaParams();
        }

        private void InitializeFromString(string content)
        {
            _jsonModel = JsonConvert.DeserializeObject<JsonServiceAccountModel>(content);

            if (_jsonModel == null)
            {
                throw new FileLoadException("Incorrect json file was provided");
            }
            FillRsaParams();
        }

        private void FillRsaParams()
        {
            string jwt = string.Empty;
            RsaPrivateCrtKeyParameters key;
            using (StringReader sr = new StringReader(_jsonModel.PrivateKey))
            {
                PemReader pr = new PemReader(sr);
                key = (RsaPrivateCrtKeyParameters)pr.ReadObject();
            }

            _rsaParam = key.ToRSAParameters();
        }
    }
}

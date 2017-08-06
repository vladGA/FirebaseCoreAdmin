using FirebaseCoreAdmin.Configurations.ServiceAccounts;
using FirebaseCoreAdmin.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirebaseCoreAdmin.Configurations.AuthPayload
{
    public class CustomTokenPayloadGenerator : PayloadGenerator
    {
        private readonly IServiceAccountCredentials _creadentials;
        private readonly IFirebaseConfiguration _configuration;

        public CustomTokenPayloadGenerator(IServiceAccountCredentials credentials, IFirebaseConfiguration configuration)
        {
            _creadentials = credentials;
            _configuration = configuration;
        }

        public sealed override IDictionary<string, string> GetPayload(IDictionary<string, string> additionalPayload = null)
        {
            var iat = DateTime.Now.ToUnixSeconds();
            var exp = (DateTime.Now + _configuration.CustomTokenTTL).ToUnixSeconds();

            AddToPayload("iss", _creadentials.GetServiceAccountEmail());
            AddToPayload("sub", _creadentials.GetServiceAccountEmail());
            AddToPayload("aud", _configuration.CustomTokenPath.AbsoluteUri);
            AddToPayload("iat", iat.ToString());
            AddToPayload("exp", exp.ToString());

            return base.GetPayload(additionalPayload);
        }
    }
}

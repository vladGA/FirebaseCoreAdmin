namespace FirebaseCoreAdmin.Configurations.AuthPayload
{
    using System.Collections.Generic;

    public class PermissionAuthPayloadGenerator : PayloadGenerator
    {
        private readonly string _jwtToken;
        public PermissionAuthPayloadGenerator(string jwtToken)
        {
            _jwtToken = jwtToken;
        }
        public override IDictionary<string, string> GetPayload(IDictionary<string, string> additionalPayload = null)
        {
            AddToPayload("grant_type", "urn:ietf:params:oauth:grant-type:jwt-bearer");
            AddToPayload("assertion", _jwtToken);
            return base.GetPayload(additionalPayload);
        }
    }
}

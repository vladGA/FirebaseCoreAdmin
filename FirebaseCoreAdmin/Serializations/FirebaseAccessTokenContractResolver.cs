
namespace FirebaseCoreAdmin.Serializations
{
    using Newtonsoft.Json.Serialization;
    using System.Collections.Generic;

    public class FirebaseAccessTokenContractResolver : DefaultContractResolver
    {
        private readonly static Dictionary<string, string> _propertyMappings = new Dictionary<string, string>
        {
            { "AccessToken","access_token"},
            { "TokenType", "token_type"},
            { "ExpiresIn", "expires_in"}
        };

        protected override string ResolvePropertyName(string propertyName)
        {
            if (!_propertyMappings.TryGetValue(propertyName, out string resolvedName))
            {
                return base.ResolvePropertyName(propertyName);
            }

            return resolvedName;
        }

    }
}

namespace FirebaseCoreAdmin.Configurations
{
    using System;
    using FirebaseCoreAdmin.JWT.Encryption;
    using System.Collections.Generic;

    public interface IFirebaseConfiguration
    {
        Uri GoogleOAuthTokenPath { get; }
        Uri CustomTokenPath { get; }
        TimeSpan AccessTokenTTL { get; }
        TimeSpan CustomTokenTTL { get; }
        IList<string> AllowedGoogleScopes { get; }
        string GoogleScopeDelimiter { get; }
        IJWTProvider JwtTokenProvider { get; }
        string FirebaseHost { get; }
        string StorageBaseAuthority { get; }
        string StorageBaseAuthority2 { get; }
    }
}

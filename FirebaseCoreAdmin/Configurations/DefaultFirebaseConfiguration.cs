namespace FirebaseCoreAdmin.Configurations
{
    using System;
    using System.Collections.Generic;
    using FirebaseCoreAdmin.JWT.Encryption;
    using FirebaseCoreAdmin.Encryption.JWT.Providers;

    public class DefaultFirebaseConfiguration : IFirebaseConfiguration
    {
        private readonly GoogleServiceAccess _requestedAccess;

        public DefaultFirebaseConfiguration()
        {
            _requestedAccess = GoogleServiceAccess.Full;
        }

        public DefaultFirebaseConfiguration(GoogleServiceAccess requestedAccess)
        {
            _requestedAccess = requestedAccess;
        }
        public Uri GoogleOAuthTokenPath => new Uri("https://www.googleapis.com/oauth2/v4/token");

        public Uri CustomTokenPath => new Uri("https://identitytoolkit.googleapis.com/google.identity.identitytoolkit.v1.IdentityToolkit");

        public TimeSpan AccessTokenTTL => new TimeSpan(0, 4, 0);

        public TimeSpan CustomTokenTTL => new TimeSpan(0, 60, 0);

        public IList<string> AllowedGoogleScopes
        {
            get
            {
                var scopeList = new List<string>() { "https://www.googleapis.com/auth/userinfo.email" };

                if (GoogleServiceAccess.DatabaseOnly == (GoogleServiceAccess.DatabaseOnly & _requestedAccess))
                {
                    scopeList.Add("https://www.googleapis.com/auth/firebase");
                    scopeList.Add("https://www.googleapis.com/auth/firebase.database");
                }

                if (GoogleServiceAccess.StorageOnly == (GoogleServiceAccess.StorageOnly & _requestedAccess))
                {
                    scopeList.Add("https://www.googleapis.com/auth/devstorage.full_control");
                }

                return scopeList;
            }
        }

        public string GoogleScopeDelimiter => " ";

        public IJWTProvider JwtTokenProvider => new JoseJwtProvider();

        public string FirebaseHost => "firebaseio.com";

        public string StorageBaseAuthority => "https://storage.googleapis.com";

        public string StorageBaseAuthority2 => " https://www.googleapis.com/storage";
    }
}

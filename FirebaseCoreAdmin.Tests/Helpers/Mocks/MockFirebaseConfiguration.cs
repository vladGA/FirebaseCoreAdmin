using FirebaseCoreAdmin.Configurations;
using System;
using System.Collections.Generic;
using System.Text;
using FirebaseCoreAdmin.JWT.Encryption;

namespace FirebaseCoreAdmin.Tests.Helpers.Mocks
{
    public class MockFirebaseConfiguration : IFirebaseConfiguration
    {
        private Uri _googleOAuthTokenPath = new Uri("http://localhost/Auth");
        private Uri _customTokenPath = new Uri("http://localhost/token");

        public Uri GoogleOAuthTokenPath => _googleOAuthTokenPath;

        public Uri CustomTokenPath => _customTokenPath;

        public void SetGoogleOAuthTokenPath(Uri value)
        {
            _googleOAuthTokenPath = value;
        }

        public TimeSpan AccessTokenTTL => new TimeSpan(0, 4, 0);

        public TimeSpan CustomTokenTTL => new TimeSpan(0, 4, 0);

        public IList<string> AllowedGoogleScopes => new List<string>()
        {
            "http://localhost"
        };

        public string GoogleScopeDelimiter => " ";

        public IJWTProvider JwtTokenProvider => new MockJwtProvider();

        public string FirebaseHost => "firebaseio.com";

        public string StorageBaseAuthority => "https://www.googleapis.com/storage/v1/b/";

        public string StorageBaseAuthority2 => "https://www.googleapis.com/storage/v1/b/";
    }
}

using FirebaseCoreAdmin.Exceptions;
using FirebaseCoreAdmin.Tests.Helpers.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FirebaseCoreAdmin.Tests
{
    public class FirebaseHttpClientTest
    {
        [Fact]
        public void Send2LOTokenRequestAsync_Throws_When_Incorrect_AuthUri()
        {
            var serviceAccount = new MockServiceAccountCredentials();
            var firebaseAccount = new MockFirebaseConfiguration();
            firebaseAccount.SetGoogleOAuthTokenPath(new Uri("http://localhost"));

            using (var client = new MockFirebaseHttpClient(serviceAccount, firebaseAccount))
            {
                Assert.Throws(typeof(AggregateException), () => client.Send2LOTokenRequestAsync().Result);
            }
        }
        [Fact]
        public void Send2LOTokenRequestAsync_Receive_AccessToken()
        {
            var serviceAccount = new MockServiceAccountCredentials();
            var firebaseAccount = new MockFirebaseConfiguration();

            using (var client = new MockFirebaseHttpClient(serviceAccount, firebaseAccount))
            {
                var token = client.Send2LOTokenRequestAsync().Result;
                Assert.NotNull(token);
                Assert.Equal(token.AccessToken, "dummy");
            }
        }


    }
}

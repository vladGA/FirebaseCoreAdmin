using FirebaseCoreAdmin.Firebase.Commands;
using FirebaseCoreAdmin.Firebase.Database;
using FirebaseCoreAdmin.HttpClients;
using FirebaseCoreAdmin.Tests.Helpers.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FirebaseCoreAdmin.Tests
{
    public class CommandsTest : IDisposable
    {
        private IFirebaseHttpClient _httpClient;

        public CommandsTest()
        {
            var serviceAccount = new MockServiceAccountCredentials();
            var firebaseAccount = new MockFirebaseConfiguration();
            _httpClient = new MockFirebaseHttpClient(serviceAccount, firebaseAccount);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        [Fact]
        public void Throws_IfNullPath_Specified()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new FirebaseAdminRef(_httpClient, ""));
        }

        [Fact]
        public void Throws_IdAddCommand_WithNullKey()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new FirebaseAdminRef(_httpClient, "test").Add("", ""));
        }

        [Fact]
        public void AddSuccessfully()
        {
            new FirebaseAdminRef(_httpClient, "test").Add("key", "val");
            Assert.True(true);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("     ")]
        public void OrderBy_InvalidArguments(string arg)
        {
            var dbRef = new FirebaseAdminRef(_httpClient, "Base");
            Assert.Throws(typeof(ArgumentNullException), () => dbRef.OrderBy(arg));
        }


        [Fact]
        public void PushAsync_IncorrectParams()
        {
            string s = null;
            var dbRef = new FirebaseAdminRef(_httpClient, "Base");
            Assert.Throws(typeof(AggregateException), () => dbRef.PushAsync(s).Result);
        }

        [Fact]
        public void SetAsync_IncorrectParams()
        {
            string s = null;
            var dbRef = new FirebaseAdminRef(_httpClient, "Base");
            Assert.Throws(typeof(AggregateException), () => dbRef.SetAsync(s).Result);
        }


        [Fact]
        public void UpdateAsync_IncorrectParams()
        {
            string s = null;
            var dbRef = new FirebaseAdminRef(_httpClient, "Base");
            Assert.Throws(typeof(AggregateException), () => dbRef.UpdateAsync(new Dictionary<string, object>()).Result);
            Assert.Throws(typeof(AggregateException), () => dbRef.UpdateAsync(null).Result);
        }
    }
}

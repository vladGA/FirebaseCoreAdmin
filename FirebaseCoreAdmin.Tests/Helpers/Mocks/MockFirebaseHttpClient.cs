using FirebaseCoreAdmin.Configurations;
using FirebaseCoreAdmin.Configurations.ServiceAccounts;
using FirebaseCoreAdmin.HttpClients;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FirebaseCoreAdmin.Tests.Helpers.Mocks
{
    public class MockFirebaseHttpClient : FirebaseHttpClient
    {
        public MockFirebaseHttpClient(IServiceAccountCredentials serviceAccount, IFirebaseConfiguration firebaseConfig)
            : base(serviceAccount, firebaseConfig, new Uri("Https://dummy"))
        {
        }

        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            StringContent content = null;
            if (request.RequestUri.AbsoluteUri == "http://localhost/Auth")
            {
                var resp = new
                {
                    access_token = "dummy",
                    expires_in = 3600,
                    token_type = "Bearer"
                };
                var stringResp = JsonConvert.SerializeObject(resp);
                content = new StringContent(stringResp);
            }
            else
            {
                content = new StringContent("");
            }


            var r = new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = content };
            var dummyTask = Task.Run(() => r);
            return dummyTask;
        }
    }
}

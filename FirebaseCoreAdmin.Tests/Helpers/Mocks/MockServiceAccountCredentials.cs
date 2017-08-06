using FirebaseCoreAdmin.Configurations.ServiceAccounts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace FirebaseCoreAdmin.Tests.Helpers.Mocks
{
    public class MockServiceAccountCredentials : IServiceAccountCredentials
    {
        private Uri _mockAuthority = new Uri("http://localhost/base");
        public Uri Authority => _mockAuthority;

        public void SetAuthority(Uri val)
        {
            _mockAuthority = val;
        }

        public RSAParameters GetRSAParams()
        {
            return Common.MockRSAParams;
        }

        public string GetServiceAccountEmail()
        {
            return "Test Service Account";
        }

        public string GetProjectId()
        {
            return "Test_Project_Id";
        }

        public string GetDefaultBucket()
        {
            return "Test_Bucket";
        }
    }
}

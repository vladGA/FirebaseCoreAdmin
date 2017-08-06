using FirebaseCoreAdmin.JWT.Encryption;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FirebaseCoreAdmin.Tests.Helpers.Mocks
{
    public class MockJwtProvider : IJWTProvider
    {
        public string Encode(IDictionary<string, string> payload, RSA privateKey)
        {
            if (payload == null || payload.Count == 0)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            if (privateKey == null)
            {
                throw new ArgumentNullException(nameof(privateKey));
            }
            return "Test Token";
        }
    }
}

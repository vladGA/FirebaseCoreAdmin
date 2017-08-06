using FirebaseCoreAdmin.Encryption.JWT.Providers;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace FirebaseCoreAdmin.Tests
{
    public class JoseJwtProviderTest
    {
        [Fact]
        public void Throws_Encode_With_Empty_Payload()
        {
            JoseJwtProvider jwtProvider = new JoseJwtProvider();

            Assert.Throws(typeof(ArgumentNullException),
                () =>
                {
                    using (var rsa = new RSACryptoServiceProvider())
                    {
                        rsa.ImportParameters(Helpers.Common.MockRSAParams);
                        jwtProvider.Encode(new Dictionary<string, string>(), rsa);
                    }
                }
                );

            Assert.Throws(typeof(ArgumentNullException), () =>
            {
                using (var rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportParameters(Helpers.Common.MockRSAParams);
                    jwtProvider.Encode(null, rsa);
                }
            });
        }

        [Fact]
        public void Throws_Encode_With_Empty_Key()
        {
            JoseJwtProvider jwtProvider = new JoseJwtProvider();

            Assert.Throws(typeof(ArgumentNullException),
                () => jwtProvider.Encode(new Dictionary<string, string>() { { "key", "val" } }, null));
        }

        [Fact]
        public void Encode_Payload()
        {
            JoseJwtProvider jwtProvider = new JoseJwtProvider();
            string token;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(Helpers.Common.MockRSAParams);
                token = jwtProvider.Encode(new Dictionary<string, string>() { { "key", "val" } }, rsa);
            }

            Assert.NotEmpty(token);
        }

    }
}

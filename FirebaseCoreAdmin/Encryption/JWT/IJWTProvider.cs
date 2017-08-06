namespace FirebaseCoreAdmin.JWT.Encryption
{
    using System.Collections.Generic;
    using System.Security.Cryptography;

    public interface IJWTProvider
    {
        string Encode(IDictionary<string, string> payload, RSA privateKey);
    }
}

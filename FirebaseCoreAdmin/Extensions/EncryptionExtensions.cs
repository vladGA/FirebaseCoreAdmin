
namespace FirebaseCoreAdmin.Extensions
{
    using Org.BouncyCastle.Crypto.Parameters;
    using System.Security.Cryptography;

    public static class EncryptionExtensions
    {
        public static RSAParameters ToRSAParameters(this RsaPrivateCrtKeyParameters privKey)
        {
            RSAParameters rp = new RSAParameters();
            rp.Modulus = privKey.Modulus.ToByteArrayUnsigned();
            rp.Exponent = privKey.PublicExponent.ToByteArrayUnsigned();
            rp.D = privKey.Exponent.ToByteArrayUnsigned();
            rp.P = privKey.P.ToByteArrayUnsigned();
            rp.Q = privKey.Q.ToByteArrayUnsigned();
            rp.DP = privKey.DP.ToByteArrayUnsigned();
            rp.DQ = privKey.DQ.ToByteArrayUnsigned();
            rp.InverseQ = privKey.QInv.ToByteArrayUnsigned();
            return rp;
        }
    }
}

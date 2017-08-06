namespace FirebaseCoreAdmin.Configurations.ServiceAccounts
{
    using System.Security.Cryptography;

    public interface IServiceAccountCredentials
    {
        RSAParameters GetRSAParams();
        string GetServiceAccountEmail();
        string GetProjectId();
        string GetDefaultBucket();
    }
}

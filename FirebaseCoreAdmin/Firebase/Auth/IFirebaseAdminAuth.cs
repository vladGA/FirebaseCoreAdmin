namespace FirebaseCoreAdmin.Firebase.Auth
{
    using FirebaseCoreAdmin.HttpClients;
    using System;
    using System.Threading.Tasks;

    public interface IFirebaseAdminAuth : IDisposable
    {
        void Authenticate();
        Task AuthenticateAsync();
        void AddFirebaseHttpClient(IFirebaseHttpClient client);
        string CreateCustomToken(long userId);
    }
}
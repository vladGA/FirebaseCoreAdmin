
namespace FirebaseCoreAdmin.Firebase.Database
{
    using FirebaseCoreAdmin.HttpClients;
    using System.Collections.Generic;

    public interface IFirebaseAdminRef
    {
        IFirebaseHttpClient HttpClient { get; }

        string Path { get; }

        void Add(string key, string value);

        void AddBool(string key, bool value);

        void AddString(string key, string value);

        IList<KeyValuePair<string, string>> GetQueryStore();
    }
}

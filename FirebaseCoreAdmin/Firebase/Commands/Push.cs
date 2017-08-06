
namespace FirebaseCoreAdmin.Firebase.Commands
{
    using System;
    using FirebaseCoreAdmin.Firebase.Database;
    using System.Threading.Tasks;

    public static partial class CommandExtensions
    {
        public async static Task<string> PushAsync<T>(this IFirebaseAdminRef firebaseRef, T content)
        {
            if(content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            return await firebaseRef.HttpClient.PushToPathAsync(firebaseRef.Path, content);
        }

        public static string Push<T>(this IFirebaseAdminRef firebaseRef, T content)
        {
            return PushAsync(firebaseRef, content).Result;
        }
    }
}

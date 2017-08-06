namespace FirebaseCoreAdmin.Firebase.Commands
{
    using System;
    using FirebaseCoreAdmin.Firebase.Database;
    using System.Threading.Tasks;

    public static partial class CommandExtensions
    {

        public async static Task<T> SetAsync<T>(this IFirebaseAdminRef firebaseRef, T content)
        {
            if(content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            return await firebaseRef.HttpClient.SetToPathAsync<T>(firebaseRef.Path, content);
        }

        public static T Set<T>(this IFirebaseAdminRef firebaseRef, T content)
        {
            return SetAsync(firebaseRef, content).Result;
        }
    }
}

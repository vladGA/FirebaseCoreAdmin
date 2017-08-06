namespace FirebaseCoreAdmin.Firebase.Commands
{
    using FirebaseCoreAdmin.Firebase.Database;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public static partial class CommandExtensions
    {
        public async static Task<object> UpdateAsync(this IFirebaseAdminRef firebaseRef, Dictionary<string, object> content)
        {
            if (content == null || content.Count == 0)
            {
                throw new ArgumentNullException(nameof(content));
            }
            return await firebaseRef.HttpClient.UpdatePathAsync(firebaseRef.Path, content);
        }

        public static object Update(this IFirebaseAdminRef firebaseRef, Dictionary<string, object> content)
        {
            return firebaseRef.HttpClient.UpdatePathAsync(firebaseRef.Path, content).Result;
        }
    }
}

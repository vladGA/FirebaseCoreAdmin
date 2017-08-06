namespace FirebaseCoreAdmin.Firebase.Commands
{
    using FirebaseCoreAdmin.Firebase.Database;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public static partial class CommandExtensions
    {
        public static T Get<T>(this IFirebaseAdminRef firebaseRef)
        {
            return GetAsync<T>(firebaseRef).Result;
        }

        public async static Task<T> GetAsync<T>(this IFirebaseAdminRef firebaseRef)
        {
            var uri = PrepareUri(firebaseRef);
            return await firebaseRef.HttpClient.GetFromPathAsync<T>(uri);
        }

        public async static Task<IList<T>> GetWithKeyInjectedAsync<T>(this IFirebaseAdminRef firebaseRef) where T : KeyEntity
        {
            var uri = PrepareUri(firebaseRef);
            return await firebaseRef.HttpClient.GetFromPathAsyncWithKeyInjected<T>(uri);
        }

        public static IList<T> GetWithKeyInjected<T>(this IFirebaseAdminRef firebaseRef) where T : KeyEntity
        {
            return GetWithKeyInjectedAsync<T>(firebaseRef).Result;
        }

        private static Uri PrepareUri(IFirebaseAdminRef firebaseRef)
        {
            var queryStore = firebaseRef.GetQueryStore();
            var queryParams = new StringBuilder("?");

            foreach (var param in queryStore)
            {
                queryParams.Append($"{param.Key}={param.Value}&");
            }

            return new Uri(firebaseRef.Path + queryParams.ToString(), UriKind.Relative);
        }

    }
}

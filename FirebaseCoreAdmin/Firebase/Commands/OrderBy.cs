namespace FirebaseCoreAdmin.Firebase.Commands
{
    using FirebaseCoreAdmin.Firebase.Database;
    using System;

    public static partial class CommandExtensions
    {
        public static IFirebaseAdminRef OrderBy(this IFirebaseAdminRef firebaseRef, string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }
            firebaseRef.Add("orderBy", $"\"{value}\"");
            return firebaseRef;
        }
    }
}

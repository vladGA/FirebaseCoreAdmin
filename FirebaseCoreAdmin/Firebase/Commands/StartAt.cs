namespace FirebaseCoreAdmin.Firebase.Commands
{
    using FirebaseCoreAdmin.Firebase.Database;
    using System;

    public static partial class CommandExtensions
    {
        private const string StartAtValue = "startAt";

        public static IFirebaseAdminRef StartAt(this IFirebaseAdminRef firebaseRef, int value)
        {
            firebaseRef.Add(StartAtValue, value.ToString());
            return firebaseRef;
        }

        public static IFirebaseAdminRef StartAt(this IFirebaseAdminRef firebaseRef, long value)
        {
            firebaseRef.Add(StartAtValue, value.ToString());
            return firebaseRef;
        }

        public static IFirebaseAdminRef StartAt(this IFirebaseAdminRef firebaseRef, float value)
        {
            firebaseRef.Add(StartAtValue, value.ToString());
            return firebaseRef;
        }

        public static IFirebaseAdminRef StartAt(this IFirebaseAdminRef firebaseRef, decimal value)
        {
            firebaseRef.Add(StartAtValue, value.ToString());
            return firebaseRef;
        }

        public static IFirebaseAdminRef StartAt(this IFirebaseAdminRef firebaseRef, string value)
        {
            firebaseRef.AddString(StartAtValue, value);
            return firebaseRef;
        }

        public static IFirebaseAdminRef StartAt(this IFirebaseAdminRef firebaseRef, bool value)
        {
            firebaseRef.AddBool(StartAtValue, value);
            return firebaseRef;
        }
    }
}

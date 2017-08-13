namespace FirebaseCoreAdmin.Firebase.Commands
{
    using FirebaseCoreAdmin.Firebase.Database;
    using System;

    public static partial class CommandExtensions
    {
        private const string EndAtValue = "endAt";

        public static IFirebaseAdminRef EndAt(this IFirebaseAdminRef firebaseRef, int value)
        {
            firebaseRef.Add(EndAtValue, value.ToString());
            return firebaseRef;
        }

        public static IFirebaseAdminRef EndAt(this IFirebaseAdminRef firebaseRef, long value)
        {
            firebaseRef.Add(EndAtValue, value.ToString());
            return firebaseRef;
        }

        public static IFirebaseAdminRef EndAt(this IFirebaseAdminRef firebaseRef, float value)
        {
            firebaseRef.Add(EndAtValue, value.ToString());
            return firebaseRef;
        }

        public static IFirebaseAdminRef EndAt(this IFirebaseAdminRef firebaseRef, decimal value)
        {
            firebaseRef.Add(EndAtValue, value.ToString());
            return firebaseRef;
        }

        public static IFirebaseAdminRef EndAt(this IFirebaseAdminRef firebaseRef, string value)
        {
            firebaseRef.AddString(EndAtValue, value);
            return firebaseRef;
        }

        public static IFirebaseAdminRef EndAt(this IFirebaseAdminRef firebaseRef, bool value)
        {
            firebaseRef.AddBool(EndAtValue, value);
            return firebaseRef;
        }
    }
}

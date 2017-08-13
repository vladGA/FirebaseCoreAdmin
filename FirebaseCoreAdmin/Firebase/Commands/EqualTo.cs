namespace FirebaseCoreAdmin.Firebase.Commands
{
    using FirebaseCoreAdmin.Firebase.Database;
    using System;

    public static partial class CommandExtensions
    {
        public const string EqualToValue = "equalTo";
        public static IFirebaseAdminRef EqualTo(this IFirebaseAdminRef firebaseRef, int value)
        {
            firebaseRef.Add(EqualToValue, value.ToString());
            return firebaseRef;
        }

        public static IFirebaseAdminRef EqualTo(this IFirebaseAdminRef firebaseRef, long value)
        {
            firebaseRef.Add(EqualToValue, value.ToString());
            return firebaseRef;
        }

        public static IFirebaseAdminRef EqualTo(this IFirebaseAdminRef firebaseRef, float value)
        {
            firebaseRef.Add(EqualToValue, value.ToString());
            return firebaseRef;
        }

        public static IFirebaseAdminRef EqualTo(this IFirebaseAdminRef firebaseRef, decimal value)
        {
            firebaseRef.Add(EqualToValue, value.ToString());
            return firebaseRef;
        }

        public static IFirebaseAdminRef EqualTo(this IFirebaseAdminRef firebaseRef, string value)
        {
            firebaseRef.AddString(EqualToValue, value);
            return firebaseRef;
        }

        public static IFirebaseAdminRef EqualTo(this IFirebaseAdminRef firebaseRef, bool value)
        {
            firebaseRef.AddBool(EqualToValue, value);
            return firebaseRef;
        }
    }
}

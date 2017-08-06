namespace FirebaseCoreAdmin
{
    using FirebaseCoreAdmin.Firebase.Auth;
    using FirebaseCoreAdmin.Firebase.Database;
    using FirebaseCoreAdmin.Firebase.Storage;

    public interface IFirebaseAdmin
    {
        IFirebaseAdminAuth Auth { get; }
        IFirebaseAdminDatabase Database { get; }
        IGoogleStorage Storage { get; }
    }
}

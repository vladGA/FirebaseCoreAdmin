namespace FirebaseCoreAdmin
{
    using System;
    using FirebaseCoreAdmin.Configurations.ServiceAccounts;
    using FirebaseCoreAdmin.Firebase.Auth;
    using FirebaseCoreAdmin.Firebase.Database;
    using FirebaseCoreAdmin.Firebase.Storage;

    public class FirebaseAdmin : IFirebaseAdmin, IDisposable
    {
        #region Fields  

        private IServiceAccountCredentials _credentials;
        private IFirebaseAdminAuth _auth;
        private IFirebaseAdminDatabase _database;
        private IGoogleStorage _storage;

        private GoogleServiceAccess _requestedAccess;

        #endregion

        #region Properties

        public IFirebaseAdminAuth Auth => _auth;
        public IFirebaseAdminDatabase Database => _database;
        public IGoogleStorage Storage => _storage;

        #endregion

        #region Constructors

        public FirebaseAdmin(IServiceAccountCredentials credentials) : this(credentials, GoogleServiceAccess.Full)
        {
        }

        public FirebaseAdmin(IServiceAccountCredentials credentials, GoogleServiceAccess access)
        {
            Initialize(credentials, access);
        }

        #endregion

        #region IDisposable Methods

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _auth.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~FirebaseAdmin()
        {
            Dispose(false);
        }

        #endregion

        #region Private Helpers

        private void Initialize(IServiceAccountCredentials credentials, GoogleServiceAccess access)
        {
            _requestedAccess = access;
            _credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
            _auth = new FirebaseAdminAuth();

            if (GoogleServiceAccess.DatabaseOnly == (_requestedAccess & GoogleServiceAccess.DatabaseOnly))
                _database = new FirebaseAdminDatabase(_auth, _credentials);

            if (GoogleServiceAccess.StorageOnly == (_requestedAccess & GoogleServiceAccess.StorageOnly))
                _storage = new GoogleCloudStorage(_auth, _credentials);
        }

        #endregion
    }

    [Flags]
    public enum GoogleServiceAccess
    {
        DatabaseOnly = 0b0001,
        StorageOnly = 0b0010,
        Full = 0b0011
    }
}

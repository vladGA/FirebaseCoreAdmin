namespace FirebaseCoreAdmin.Exceptions
{
    using System;
    using System.Net;

    public class FirebaseHttpException : Exception
    {
        public FirebaseHttpException(string message)
            : base(message)
        {
        }
        public FirebaseHttpException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        public FirebaseHttpException(Exception innerException)
            : base(innerException.Message, innerException)
        {
        }
        public FirebaseHttpException(HttpStatusCode statusCode, string responseBody)
            : base($"Request to firebase. Status code={statusCode}, Response={responseBody}")
        {
        }
    }
}

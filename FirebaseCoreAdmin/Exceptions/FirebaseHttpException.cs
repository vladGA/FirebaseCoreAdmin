namespace FirebaseCoreAdmin.Exceptions
{
    using System;
    using System.Net;
    using System.Net.Http;

    public class FirebaseHttpException : Exception
    {
        public HttpRequestMessage RequestMessage { get; private set; }
        public HttpResponseMessage ResponseMessage { get; private set; }
        public string ResponseContent { get; private set; }

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
        public FirebaseHttpException(string responseBody,
            HttpRequestMessage request, HttpResponseMessage response)
        {
            ResponseContent = responseBody;
            RequestMessage = request;
            ResponseMessage = response;
        }
    }
}

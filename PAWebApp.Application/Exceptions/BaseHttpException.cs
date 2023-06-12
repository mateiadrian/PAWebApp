using System.Net;

namespace PAWebApp.Application.Exceptions
{
    [Serializable]
    public abstract class BaseHttpException : Exception
    {
        public BaseHttpException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}

using System.Net;

namespace PAWebApp.Application.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : BaseHttpException
    {
        public EntityNotFoundException()
            : base(HttpStatusCode.NotFound, string.Empty)
        {
        }

        public EntityNotFoundException(string message)
            : base(HttpStatusCode.NotFound, message)
        {
        }
    }
}

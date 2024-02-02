using System.Net;

namespace Application.Common.Errors
{
    public class BadRequestError
    {
        public HttpStatusCode StatusCode { get; set; }
        public required string Message { get; set; }

        public BadRequestError()
        {
            StatusCode = HttpStatusCode.BadRequest;
        }
    }
}

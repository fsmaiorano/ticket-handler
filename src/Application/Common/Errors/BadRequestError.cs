using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

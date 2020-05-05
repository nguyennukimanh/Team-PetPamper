using System.Net;

namespace PetPamper.Models.Abstractions
{
    public class BaseResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }

        public string ErrorMessage { get; set; }

        public bool Status
        {
            get
            {
                return string.IsNullOrEmpty(ErrorMessage);
            }
        }

        public BaseResponseModel()
        {
            StatusCode = HttpStatusCode.OK;
        }

        public BaseResponseModel(string errorMessage)
        {
            ErrorMessage = errorMessage;
            StatusCode = string.IsNullOrEmpty(errorMessage) ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        }
    }
}
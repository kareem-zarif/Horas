namespace Horas.Api.Helpers
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null, string details = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefualtMassageForStatusCode(StatusCode);
            Details = details;

        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }

        private string GetDefualtMassageForStatusCode(int statusCode)
        {

            return statusCode switch
            {
                400 => "Bad request , you have Made",
                401 => "Authorized , you are not",
                404 => "Resource Found , it was not",
                500 => "Server Errors .mahmoud abdullatif  ",
                _ => " * "
            };
        }
    }
}

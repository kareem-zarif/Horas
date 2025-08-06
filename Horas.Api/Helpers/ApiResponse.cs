namespace Horas.Api.Helpers
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefualtMassageForStatusCode(StatusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GetDefualtMassageForStatusCode(int statusCode)
        {

            return statusCode switch
            {
                400 => "Bad request , you have Made",
                401 => "Authorized , you are not",
                404 => "Resource Found , it was not",
                500 => "Server Errors are the dark side .mahmoud abdullatif  ",
                _ => " * "
            };
        }
    }
}

using System.Net;

namespace Majunga.Libraries.Client.HttpRepositories
{
    public class Result
    {
        public Result(bool isSuccessful, HttpStatusCode statusCode)
        {
            IsSuccessful = isSuccessful;
            StatusCode = statusCode;
        }

        public bool IsSuccessful { get; }
        public HttpStatusCode StatusCode { get; }
    }
}
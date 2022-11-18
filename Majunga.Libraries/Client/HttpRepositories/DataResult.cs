using System.Net;

namespace Majunga.Libraries.Client.HttpRepositories
{
    public class DataResult<T> : Result
        where T : class
    {
        public DataResult(bool isSuccessful, HttpStatusCode statusCode, T? data) : base(isSuccessful, statusCode)
        {
            Data = data;
        }

        public T? Data { get; }
    }
}

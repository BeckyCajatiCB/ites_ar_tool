using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace ArTool.Exceptions
{
    public class ClientException : Exception
    {
        public HttpStatusCode? StatusCode { get; set; }

        public JObject Errors { get; set; }

        public ClientException(string message) : this(message, null, null, null)
        {
        }

        public ClientException(string message, Exception innerException, HttpStatusCode? statusCode, JObject errors) :
            base(message, innerException)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }

}

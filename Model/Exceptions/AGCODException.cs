using Newtonsoft.Json;
using System.Net;

namespace Model.Exceptions
{
    [Serializable]
    public class AGCODException
    {
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("errorType")]
        public string ErrorType { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
        [JsonProperty("statusCode")]
        public HttpStatusCode StatusCode { get; set; }
        [JsonProperty("errorException")]
        public Exception ErrorException { get; set; }
    }
}

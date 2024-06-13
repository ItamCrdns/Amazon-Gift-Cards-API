using Model.Exceptions;
using Newtonsoft.Json;

namespace Model.Response
{
    [Serializable]
    [JsonObject("Response")]
    public class AGCODResponse<T>
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        public AGCODException Exception { get; set; }
        public T Data { get; set; }

        public bool Success
        {
            get
            {
                return "SUCCESS".Equals(Status);
            }
        }
    }
}

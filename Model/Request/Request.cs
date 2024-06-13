using Newtonsoft.Json;

namespace Model.Request
{
    [Serializable]
    public class Request
    {
        [JsonProperty("partnerId")]
        public string PartnerId { get; set; }
    }
}

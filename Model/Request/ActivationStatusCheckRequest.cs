using Newtonsoft.Json;

namespace Model.Request
{
    [Serializable]
    public class ActivationStatusCheckRequest : Request
    {
        [JsonProperty("statusCheckRequestId")]
        public string StatusCheckRequestId { get; set; }
        [JsonProperty("cardNumber")]
        public string CardNumber { get; set; }
    }
}


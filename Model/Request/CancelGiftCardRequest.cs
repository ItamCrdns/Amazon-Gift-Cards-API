using Newtonsoft.Json;

namespace Model.Request
{
    [Serializable]
    public class CancelGiftCardRequest : Request
    {
        [JsonProperty("creationRequestId")]
        public string CreationRequestId { get; set; }

        [JsonProperty("gcId")]
        public string GcId { get; set; }
    }
}

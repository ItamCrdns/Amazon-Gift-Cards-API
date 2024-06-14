using Newtonsoft.Json;

namespace Model.Response
{
    [Serializable]
    public class CancelGiftCardResponse : AGCODResponse<CancelGiftCardResponse>
    {
        [JsonProperty("creationRequestId")]
        public string CreationRequestId { get; set; }

        [JsonProperty("gcId")]
        public string GcId { get; set; }
    }
}

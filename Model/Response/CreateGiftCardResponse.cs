using Model.Entity;
using Newtonsoft.Json;

namespace Model.Response
{
    [Serializable]
    public class CreateGiftCardResponse : AGCODResponse<CreateGiftCardResponse>
    {
        [JsonProperty("creationRequestId")]
        public string CreationRequestId { get; set; }

        [JsonProperty("cardInfo")]
        public CardInformation CardInfo { get; set; }

        [JsonProperty("gcId")]
        public string GcId { get; set; }

        [JsonProperty("gcExpirationDate")]
        public string GcExpirationDate { get; set; }

        [JsonProperty("gcClaimCode")]
        public string GcClaimCode { get; set; }
    }
}

using Model.Entity;
using Newtonsoft.Json;

namespace Model.Response
{
    [Serializable]
    public class DeactivateGiftCardResponse : AGCODResponse<DeactivateGiftCardResponse>
    {
        [JsonProperty("activationRequestId")]
        public string ActivationRequestId { get; set; }

        [JsonProperty("cardInfo")]
        public CardInformation CardInfo { get; set; }
    }
}

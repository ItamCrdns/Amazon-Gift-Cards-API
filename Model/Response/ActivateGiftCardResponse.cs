using Model.Entity;
using Newtonsoft.Json;

namespace Model.Response
{
    [Serializable]
    public class ActivateGiftCardResponse : AGCODResponse<ActivateGiftCardResponse>
    {
        [JsonProperty("activationRequestId")]
        public string ActivationRequestId { get; set; }

        [JsonProperty("cardInfo")]
        public CardInformation CardInfo { get; set; }
    }
}

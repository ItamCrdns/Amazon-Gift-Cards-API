using Newtonsoft.Json;

namespace Model.Request
{
    [Serializable]
    public class DeactivateGiftCardRequest : Request
    {
        [JsonProperty("activationRequestId")]
        public string ActivationRequestId { get; set; }

        [JsonProperty("cardNumber")]
        public string CardNumber { get; set; }
    }
}

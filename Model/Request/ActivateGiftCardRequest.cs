using Model.Entity;
using Newtonsoft.Json;

namespace Model.Request
{
    [Serializable]
    public class ActivateGiftCardRequest : Request
    {
        [JsonProperty("activationRequestId")]
        public string ActivationRequestId { get; set; }

        [JsonProperty("cardNumber")]
        public string CardNumber { get; set; }

        [JsonProperty("value")]
        public CardValue Value { get; set; }

        public ActivateGiftCardRequest()
        {
            Value = new CardValue();
        }
    }
}

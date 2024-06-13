using Model.Entity;
using Newtonsoft.Json;

namespace Model.Request
{
    [Serializable]
    public class CreateGiftCardRequest : Request
    {
        [JsonProperty("creationRequestId")]
        public string CreationRequestId { get; set; }

        [JsonProperty("value")]
        public CardValue Value { get; set; }

        public CreateGiftCardRequest()
        {
            Value = new CardValue();
        }
    }
}

using Newtonsoft.Json;

namespace Model.Entity
{
    [Serializable]
    public class CardInformation
    {
        [JsonProperty("value")]
        public CardValue Value { get; set; }

        [JsonProperty("cardStatus")]
        public string CardStatus { get; set; }

        [JsonProperty("cardNumber")]
        public string CardNumber { get; set; }
    }
}

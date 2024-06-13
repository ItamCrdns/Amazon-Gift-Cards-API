using Newtonsoft.Json;

namespace Model.Entity
{
    [Serializable]
    public class CardValue
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }
    }
}

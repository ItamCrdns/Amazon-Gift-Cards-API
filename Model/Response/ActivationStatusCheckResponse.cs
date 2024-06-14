using Model.Entity;
using Newtonsoft.Json;

namespace Model.Response
{
    [Serializable]
    public class ActivationStatusCheckResponse : AGCODResponse<ActivationStatusCheckResponse>
    {
        [JsonProperty("statusCheckRequestId")]
        public string StatusCheckRequestId { get; set; }

        [JsonProperty("cardInfo")]
        public CardInformation CardInfo { get; set; }
    }
}

using Model.Response;

namespace Amazon_Gift_Cards_API.Interface
{
    public interface ICard
    {
        public AGCODResponse<CreateGiftCardResponse> CreateGiftCard(decimal amount, string currencyCode);
        public AGCODResponse<ActivationStatusCheckResponse> ActivationStatusCheck(string id, string cardNumber);
        public AGCODResponse<CancelGiftCardResponse> CancelGiftCard(string id, string giftCodeId);
        public AGCODResponse<ActivateGiftCardResponse> ActivateGiftCard(string id, string cardNumber, decimal amount, string currencyCode);
        public AGCODResponse<DeactivateGiftCardResponse> DeactivateGiftCard(string id, string cardNumber);
    }
}

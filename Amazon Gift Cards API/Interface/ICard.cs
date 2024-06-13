using Model.Response;

namespace Amazon_Gift_Cards_API.Interface
{
    public interface ICard
    {
        public AGCODResponse<ActivateGiftCardResponse> ActivateGiftCard(string id, string cardNumber, decimal amount, string currencyCode);
        public AGCODResponse<CreateGiftCardResponse> CreateGiftCard(decimal amount, string currencyCode);
    }
}

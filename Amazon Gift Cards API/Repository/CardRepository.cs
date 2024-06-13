using Amazon_Gift_Cards_API.Authentication;
using Amazon_Gift_Cards_API.Interface;
using Model.Entity;
using Model.Exceptions;
using Model.Request;
using Model.Response;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace Amazon_Gift_Cards_API.Repository
{
    public class CardRepository : ICard
    {
        private readonly IConfiguration _configuration;
        private readonly RestClient _client;
        private readonly string? _partnerId;
        //private readonly string? _currencyCode;
        private readonly string? _accessKey;
        private readonly string? _secret;
        private readonly string? _region;
        protected readonly string _serviceName = "AGCODService";
        public CardRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new RestClient(_configuration.GetValue<string>("Amazon:AGCODUrl"));
            _partnerId = _configuration.GetValue<string>("Amazon:PartnerId");
            //_currencyCode = _configuration.GetValue<string>("Amazon:CurrenyCode");
            _accessKey = _configuration.GetValue<string>("Amazon:AWSAccessKey");
            _secret = _configuration.GetValue<string>("Amazon:AWSSecret");
            _region = _configuration.GetValue<string>("Amazon:Region");
        }

        public AGCODResponse<ActivateGiftCardResponse> ActivateGiftCard(string id, string cardNumber, decimal amount, string currencyCode)
        {
            var request = CreateRequest("ActivateGiftCard", new ActivateGiftCardRequest
            {
                PartnerId = _partnerId,
                ActivationRequestId = id,
                CardNumber = cardNumber,
                Value = new CardValue
                {
                    Amount = amount,
                    CurrencyCode = currencyCode
                }
            });

            return GetResponse<ActivateGiftCardResponse>(request);
        }

        private RestRequest CreateRequest<T>(string operation, T requestBody)
        {
            var request = new RestRequest(operation, Method.Post)
            {
                RequestFormat = DataFormat.Json
            };

            var authenticator = new AWSAuthenticator(_configuration, _accessKey, _secret, _region, _serviceName);

            request.AddBody(requestBody);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            authenticator.Authenticate(request);

            return request;
        }

        private AGCODResponse<T> GetResponse<T>(RestRequest request) where T : new()
        {
            var response = _client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK || response.ErrorException != null)
            {
                AGCODException aGCODException;
                try
                {
                    var data =  JsonConvert.DeserializeObject<AGCODException>(response.Content);

                    aGCODException = new AGCODException
                    {
                        ErrorCode = data.ErrorCode,
                        ErrorType = data.ErrorType,
                        Message = data.Message,
                        StatusCode = response.StatusCode,
                        ErrorException = response.ErrorException
                    };
                }
                catch (Exception)
                {
                    throw new ApplicationException(response.ErrorMessage, response.ErrorException);
                }

                return new AGCODResponse<T>
                {
                    Status = "ERROR",
                    Exception = aGCODException
                };
            }

            return new AGCODResponse<T>
            {
                Status = "SUCCESS",
                Data = JsonConvert.DeserializeObject<T>(response.Content)
            };
        }

        public AGCODResponse<CreateGiftCardResponse> CreateGiftCard(decimal amount, string currencyCode)
        {
            string creationRequestId = (_partnerId.Substring(0, Math.Min(_partnerId.Length, 10)) + Guid.NewGuid().ToString()).Substring(0, 40); // Generate a unique ID for each request

            var request = CreateRequest("CreateGiftCard", new CreateGiftCardRequest
            {
                CreationRequestId = creationRequestId,
                PartnerId = _partnerId,
                Value = new CardValue
                {
                    Amount = amount,
                    CurrencyCode = currencyCode
                }
            });

            return GetResponse<CreateGiftCardResponse>(request);
        }
    }
}

using Amazon_Gift_Cards_API.Repository;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Model.Entity;
using Model.Response;
using System.Net;

namespace Tests.Repository
{
    public class CardRepositoryTests
    {
        private readonly IConfiguration _configuration;
        public CardRepositoryTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
        }

        [Fact]
        public async void CreateGiftCard_ReturnsCreated()
        {
            // Arrange
            var cardRepository = new CardRepository(_configuration);

            CardValue cardValue = new()
            {
                Amount = 10.00m,
                CurrencyCode = "GBP"
            };

            // Act
            var result = cardRepository.CreateGiftCard(cardValue.Amount, cardValue.CurrencyCode);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<AGCODResponse<CreateGiftCardResponse>>();
            result.Data.Should().NotBeNull();
            result.Data.Exception.Should().BeNull();
            result.Exception.Should().BeNull();
            result.Data.Status.Should().Be("SUCCESS");
        }

        [Fact]
        public async void CreateGiftCard_ReturnsFailure()
        {
            var wrongConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Amazon:AGCODUrl", "https://agcod-v2-gamma.amazon.com" },
                    { "Amazon:PartnerId", "partnerId" },
                    { "Amazon:CurrenyCode", "USD" },
                    { "Amazon:AWSAccessKey", "accessKey" },
                    { "Amazon:AWSSecret", "secret" },
                    { "Amazon:Region", "us-east-1" }
                })
                .Build();
            // Arrange
            var cardRepository = new CardRepository(wrongConfiguration);

            CardValue cardValue = new()
            {
                Amount = 10.00m,
                CurrencyCode = "GBP"
            };

            // Act
            var result = cardRepository.CreateGiftCard(cardValue.Amount, cardValue.CurrencyCode);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<AGCODResponse<CreateGiftCardResponse>>();
            result.Data.Should().BeNull();
            result.Exception.Should().NotBeNull();
            result.Exception.Message.Should().Be("The security token included in the request is invalid.");
            result.Exception.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async void ActionStatusCheck_ReturnsSuccess()
        {
            // Arrange
            var cardRepository = new CardRepository(_configuration);

            // Act
            var result = cardRepository.ActivationStatusCheck("statusCheckRequestId", "cardNumber");

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<AGCODResponse<ActivationStatusCheckResponse>>();
            result.Data.Should().NotBeNull();
            result.Data.Exception.Should().BeNull();
            result.Exception.Should().BeNull();
            result.Data.Status.Should().Be("SUCCESS");
        }

        [Fact]
        public async void CancelGiftCard_ReturnsSuccess()
        {
            // Arrange
            var cardRepository = new CardRepository(_configuration);

            // Act
            var result = cardRepository.CancelGiftCard("creationRequestId", "A23ZP02F085DFQ");

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<AGCODResponse<CancelGiftCardResponse>>();
            result.Data.Should().NotBeNull();
            result.Data.Exception.Should().BeNull();
            result.Exception.Should().BeNull();
            result.Data.Status.Should().Be("SUCCESS");
        }

        [Fact]
        public async void CancelGiftCard_ReturnsFailure()
        {
            // Arrange
            var cardRepository = new CardRepository(_configuration);

            // Act
            var result = cardRepository.CancelGiftCard("creationRequestId", "A23ZP02F085DFQ");

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<AGCODResponse<CancelGiftCardResponse>>();
            result.Data.Should().BeNull();
            result.Exception.Should().NotBeNull();
            result.Exception.Message.Should().Be("The security token included in the request is invalid.");
            result.Exception.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async void ActivateGiftCard_ReturnsSuccess()
        {
            // Arrange
            var cardRepository = new CardRepository(_configuration);

            // Act
            var result = cardRepository.ActivateGiftCard("activationRequestId", "cardNumber", 10.00m, "GBP");

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<AGCODResponse<ActivateGiftCardResponse>>();
            result.Data.Should().NotBeNull();
            result.Data.Exception.Should().BeNull();
            result.Exception.Should().BeNull();
            result.Data.Status.Should().Be("SUCCESS");
            result.Data.CardInfo.CardStatus.Should().Be("Activated");
        }

        [Fact]
        public async void DeactivateGiftCard_ReturnsSuccess()
        {
            // Arrange
            var cardRepository = new CardRepository(_configuration);

            // Act
            var result = cardRepository.DeactivateGiftCard("deactivationRequestId", "cardNumber");

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<AGCODResponse<DeactivateGiftCardResponse>>();
            result.Data.Should().NotBeNull();
            result.Data.Exception.Should().BeNull();
            result.Exception.Should().BeNull();
            result.Data.Status.Should().Be("SUCCESS");
            result.Data.CardInfo.CardStatus.Should().Be("AwaitingActivation");
        }
    }
}

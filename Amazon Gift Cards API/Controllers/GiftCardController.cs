using Amazon_Gift_Cards_API.Interface;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using Model.Request;

namespace Amazon_Gift_Cards_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftCardController(ICard cardService) : ControllerBase
    {
        private readonly ICard _cardService = cardService;

        [HttpPost("CreateGiftCard")]
        public IActionResult CreateGiftCard([FromBody] CardValue cardValue)
        {
            var response = _cardService.CreateGiftCard(cardValue.Amount, cardValue.CurrencyCode);

            if (response.Exception != null)
                return StatusCode((int)response.Exception.StatusCode, response);

            return Ok(response);
        }

        [HttpPost("ActivationStatusCheck")]
        public IActionResult ActivationStatusCheck([FromBody] ActivationStatusCheckRequest request)
        {
            var response = _cardService.ActivationStatusCheck(request.StatusCheckRequestId, request.CardNumber);

            if (response.Exception != null)
                return StatusCode((int)response.Exception.StatusCode, response);

            return Ok(response);
        }

        [HttpDelete("CancelGiftCard")]
        public IActionResult CancelGiftCard([FromBody] CancelGiftCardRequest request)
        {
            var response = _cardService.CancelGiftCard(request.CreationRequestId, request.GcId);

            if (response.Exception != null)
                return StatusCode((int)response.Exception.StatusCode, response);

            return Ok(response);
        }

        [HttpPost("ActivateGiftCard")]
        public IActionResult ActivateGiftCard([FromBody] ActivateGiftCardRequest request)
        {
            var response = _cardService.ActivateGiftCard(request.ActivationRequestId, request.CardNumber, request.Value.Amount, request.Value.CurrencyCode);

            if (response.Exception != null)
                return StatusCode((int)response.Exception.StatusCode, response);

            return Ok(response);
        }

        [HttpDelete("DeactivateGiftCard")]
        public IActionResult DeactivateGiftCard([FromBody] DeactivateGiftCardRequest request)
        {
            var response = _cardService.DeactivateGiftCard(request.ActivationRequestId, request.CardNumber);

            if (response.Exception != null)
                return StatusCode((int)response.Exception.StatusCode, response);

            return Ok(response);
        }
    }
}

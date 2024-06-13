using Amazon_Gift_Cards_API.Interface;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;

namespace Amazon_Gift_Cards_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftCardController(ICard cardService) : ControllerBase
    {
        private readonly ICard _cardService = cardService;

        [HttpPost("create")]
        public IActionResult CreateGiftCard([FromBody] CardValue cardValue)
        {
            var response = _cardService.CreateGiftCard(cardValue.Amount, cardValue.CurrencyCode);

            if (response.Exception != null)
                return StatusCode((int)response.Exception.StatusCode, response);

            return Ok(response);
        }
    }
}

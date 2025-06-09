using Microsoft.AspNetCore.Mvc;
using Kata.Checkout.Services;
using Kata.Checkout.Models;

namespace Kata.Checkout.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CheckoutController : ControllerBase
	{
		private readonly ICheckoutService _checkoutService;

		public CheckoutController(ICheckoutService checkoutService) => _checkoutService = checkoutService;

		[HttpPost("scan/{sku}")]
		public ActionResult ScanItem(string sku)
		{
			try
			{
				_checkoutService.Scan(sku);
				return Ok(new { message = $"Item {sku} added to the checkout" });
			}
			catch (ArgumentException ex)
			{
				return BadRequest(new { error = ex.Message });
			}
		}

		[HttpGet("total")]
		public ActionResult<decimal> GetTotalPrice()
		{
			var totalPrice = _checkoutService.GetTotalPrice();
			return Ok(totalPrice);
		}

		[HttpGet("items")]
		public ActionResult<IEnumerable<Item>> GetAvailableItems()
		{
			var items = _checkoutService.GetAvailableItems();
			return Ok(items);
		}

		[HttpGet("cart")]
		public ActionResult<object> GetCart()
		{
			var cartItems = _checkoutService.GetCartItems();
			var total = _checkoutService.GetTotalPrice();
			return Ok(new { items = cartItems, total });
		}
	}
}

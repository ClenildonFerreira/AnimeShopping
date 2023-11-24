using AnimeShopping.CartAPI.Data.ValueObjects;
using AnimeShopping.CartAPI.Messages;
using AnimeShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AnimeShopping.CartAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartRepository _cartRepository;
        private ICouponRepository _couponRepository;

        public CartController(ICartRepository cartRepository, ICouponRepository couponRepository)
        {
            _cartRepository = cartRepository;
            _couponRepository = couponRepository;
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartVO>> FindById(string id)
        {
            var cart = await _cartRepository.FindCartByUserId(id);
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPost("add-cart")]
        public async Task<ActionResult<CartVO>> AddCart([FromBody] CartVO vo)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(vo);
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPut("update-cart")]
        public async Task<ActionResult<CartVO>> UpdateCart([FromBody] CartVO vo)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(vo);
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<CartVO>> RemoveCart(int id)
        {
            var status = await _cartRepository.RemoveFromCart(id);
            if (!status)
                return BadRequest();

            return Ok(status);
        }

        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartVO>> ApplyCoupon(CartVO vo)
        {
            var status = await _cartRepository.ApplyCoupon(vo.CartHeader.UserId, vo.CartHeader.CouponCode);
            if (!status)
                return NotFound();

            return Ok(status);
        }

        [HttpDelete("remove-coupon/{userId}")]
        public async Task<ActionResult<CartVO>> ApplyCoupon(string userId)
        {
            var status = await _cartRepository.RemoveCoupon(userId);
            if (!status)
                return NotFound();

            return Ok(status);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderVO>> Checkout(CheckoutHeaderVO model)
        {
            if (model?.UserId == null)
                return BadRequest();

            var cart = await _cartRepository.FindCartByUserId(model.UserId);
            if (cart == null)
                return NotFound();

            if (!string.IsNullOrEmpty(model.CouponCode))
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                CouponVO coupon = await _couponRepository.GetCoupon(
                        model.CouponCode,
                        token
                    );

                if (model.DiscountAmount != coupon.DiscountAmount)
                    return StatusCode(412);
            }

            model.CartDetails = cart.CartDetails;
            model.DateTime = DateTime.Now;

            //_rabbitMQMessageSender.SendMessage(model, QueueName.Checkout.GetDescription());

            await _cartRepository.ClearCart(model.UserId);

            return Ok(model);
        }
    }
}

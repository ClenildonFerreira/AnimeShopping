using AnimeShopping.CartAPI.Data.ValueObjects;

namespace AnimeShopping.CartAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCoupon(string couponCode, string token);
    }
}

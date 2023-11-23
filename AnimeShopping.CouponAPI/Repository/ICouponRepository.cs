using AnimeShopping.CouponAPI.Data.ValueObjects;

namespace AnimeShopping.CouponAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCouponByCouponCode(string couponCode);
    }
}

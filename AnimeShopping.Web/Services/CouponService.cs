using AnimeShopping.Web.Models;
using AnimeShopping.Web.Services.IServices;
using AnimeShopping.Web.Utils;
using System.Net.Http.Headers;

namespace AnimeShopping.Web.Services
{
    public class CouponService : ICouponService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/coupon";

        public CouponService(HttpClient client)
        {
            _client = client;
        }

        public async Task<CouponViewModel> GetCoupon(string code, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{BasePath}/{code}");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return new CouponViewModel();

            return await response.ReadContentAs<CouponViewModel>();
        }
    }
}

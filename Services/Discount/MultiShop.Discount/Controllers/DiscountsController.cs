using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.Dtos;
using MultiShop.Discount.Services;

namespace MultiShop.Discount.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> CouponList()
        {
            var values = await _discountService.GetAllCouponAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCouponById(int id)
        {
            var values = await _discountService.GetByIdCouponAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon(CreateCouponDto createCouponDto)
        {
            await _discountService.CreateCouponAsync(createCouponDto);
            return Ok("Kupon Başarıyla Oluşturuldu");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            await _discountService.DeleteCouponAsync(id);
            return Ok("Kupon Başarıyla Silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCoupon(UpdateCouponDto updateCouponDto)
        {
            await _discountService.UpdateCouponAsync(updateCouponDto);
            return Ok("Kupon Başarıyla Güncellendi");

        }

        [HttpGet("GetCodeDetailByCodeAsync")]
        public async Task<IActionResult> GetCodeDetailByCodeAsync(string code)
        {
            var values = await _discountService.GetCodeDetailByCodeAsync(code);
            return Ok(values);
        }

        [HttpGet("GetDiscountCouponCountRate")]
        public IActionResult GetDiscountCouponCountRate(string code)
        {
            var values = _discountService.GetDiscountCouponCountRate(code);
            return Ok(values);
        }

        [HttpGet("GetDiscountCouponCount")]
        public async Task<IActionResult> GetDiscountCouponCount()
        {
            var values = await _discountService.GetDiscountCouponCount();
            return Ok(values);
        }


    }
}

    using Core.DTO;
using Core.Models;
using EFLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vezeeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ManagementSettingController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public ManagementSettingController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost("AddDiscountCode")]
        public IActionResult AddDiscount([FromForm] CouponCodeDTO discountDto)
        {
            try
            {
                var discount = new CouponCode
                {
                    DiscountCode = discountDto.discountcode,
                    DiscountAmount = discountDto.discountamount,
                    ExpirationDate = discountDto.expirationedate
                };
                context.CouponCodes.Add(discount);
                context.SaveChanges();

                return Ok("Discount added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to add discount: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDiscount(int id, [FromBody] CouponCodeDTO CouponDTO)
        {
            try
            {
                var discount = context.CouponCodes.Find(id);
                if (discount == null)
                {
                    return NotFound(false);
                }
                discount.DiscountCode = CouponDTO.discountcode;
                discount.DiscountAmount = CouponDTO.discountamount;
                discount.IsActive = CouponDTO.isactive;
                discount.ExpirationDate = CouponDTO.expirationedate;

                context.SaveChanges();
                return Ok(true);
            }
            catch(Exception ex)

            { 
                return BadRequest(false);
            }       
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCoupon(int id , CouponCodeDTO CouponDTO)
        {
            try
            {
                var CouponToDelete = context.CouponCodes.Find(id);

                if (CouponToDelete == null)
                {
                    return NotFound();
                }
                context.CouponCodes.Remove(CouponToDelete);
                context.SaveChanges();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(false);
            }
        }
    }
}
 
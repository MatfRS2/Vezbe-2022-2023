using Discount.Common.DTOs;
using Discount.Common.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DiscountController : ControllerBase
{
    private readonly ICouponRepository _repository;

    public DiscountController(ICouponRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{productName}")]
    [ProducesResponseType(typeof(CouponDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CouponDTO>> GetDiscount(string productName)
    {
        var coupon = await _repository.GetDiscount(productName);
        return coupon is null ? NotFound() : Ok(coupon);
    }
}
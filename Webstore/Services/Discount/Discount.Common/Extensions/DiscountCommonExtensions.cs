using Discount.Common.Data;
using Discount.Common.DTOs;
using Discount.Common.Entities;
using Discount.Common.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Common.Extensions;

public static class DiscountCommonExtensions
{
    public static void AddDiscountCommonServices(this IServiceCollection services)
    {
        services.AddScoped<ICouponContext, CouponContext>();
        services.AddScoped<ICouponRepository, CouponRepository>();
        services.AddAutoMapper(configuration =>
        {
            configuration.CreateMap<CouponDTO, Coupon>().ReverseMap();
        });
    }
}
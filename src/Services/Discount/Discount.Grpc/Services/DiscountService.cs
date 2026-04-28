using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public  override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            // Implement your logic here
            var coupon = dbContext.Coupons.FirstOrDefault(c => c.ProductName == request.ProductName);
            if (coupon == null)
            {
                coupon = new Coupon
                {
                    ProductName = request.ProductName,
                    Amount = 0,
                    Description = "No discount available"
                };
            }
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            // Implement your logic here
            var coupon = dbContext.Coupons.FirstOrDefault(c => c.ProductName == request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon data"));
            }
            dbContext.Coupons.Remove(coupon);
            dbContext.SaveChanges();
            return new DeleteDiscountResponse { Success = true };
        }
         public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            // Implement your logic here
            var coupon = request.Adapt<Coupon>();
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon data"));
            }
            dbContext.Coupons.Update(coupon);
            dbContext.SaveChanges();
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }
         public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            // Implement your logic
            var coupon = request.Adapt<Coupon>();
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon data"));
            }
            dbContext.Coupons.Add(coupon);
            dbContext.SaveChanges();
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

    }
}

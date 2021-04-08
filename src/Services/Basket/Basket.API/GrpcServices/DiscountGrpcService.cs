using Discount.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    /// <summary>
    /// Do not inherit from proto service class because this is not a server application but a client app
    /// </summary>
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService;
        }

        public async Task<CouponModel> GetDiscount(string productName) 
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName};
            /*if (discountRequest == null)
            {
                throw new NullPointerException();
            }*/
            return await _discountProtoService.GetDiscountAsync(discountRequest);
        }
    }
}

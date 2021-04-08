using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    /// <summary>
    /// The DiscountService inherits from the auto generated service base from the proto file. 
    /// </summary>
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        //[inject]
        private readonly IMapper _mapper;
        private readonly ILogger<DiscountService> _logger;
        /// <summary>
        /// The constructor below injects three class that will be used within the c# class.
        /// 
        /// </summary>
        /// <param name="repository">This param will be used to call the CRUD methods
        ///     from the IDiscountRepository</param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public DiscountService(IDiscountRepository repository, IMapper mapper,ILogger<DiscountService> logger)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        ///
        // override methods in service 
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetDiscount(request.ProductName);

            if (coupon == null) 
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
            }
            _logger.LogInformation("Discount is retrieved for ProductName: {productName}, Amount : {amount}",coupon.ProductName, coupon.Amount);

            // var destination = _mapper.Map<Destination>(source);
            // source => coupon , destination => couponModel
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            await _repository.CreateDiscount(coupon);
            _logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            await _repository.UpdateDiscount(coupon);
            _logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _repository.DeleteDiscount(request.ProductName);
            var response = new DeleteDiscountResponse
            {
                Success = deleted
            };
            return response;
        }
    }
}

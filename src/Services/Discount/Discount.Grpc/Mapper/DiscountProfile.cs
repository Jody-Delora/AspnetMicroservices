using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Mapper
{
    public class DiscountProfile : Profile
    {
        /// <summary>
        /// - AutoMapper uses a convention-based matching algorithm to match up source 
        ///   to destination values.
        /// - AutoMapper is geared towards model projection scenarios to flatten complex
        ///   object models to DTOs and other simple objects, whose design is better
        ///   suited for serialization, communication, messaging, or simply an 
        ///   anti-corruption layer between the domain and application layer.
        /// </summary>
        /// 
    
    public DiscountProfile()
    { 
        // CreateMap<TSource, TDestination>()

        CreateMap<Coupon, CouponModel>().ReverseMap();
        // NOTE: If there is a property in CouponModel that is not in Coupon
        // CreateMap<Coupon, CouponModel>().ForMember( dest = dest.diffProperty,
        // opt => opt.MapFrom(src => src.relevantProperty));
        }
    }
}

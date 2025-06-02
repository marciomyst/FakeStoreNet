using AutoMapper;
using FakeStoreNet.Application.Features.Product.Queries;
using FakeStoreNet.Domain.Entities;

namespace FakeStoreNet.Application.Common
{
    /// <summary>
    /// AutoMapper profile for application layer mappings.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MappingProfile"/>.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rating.Rate))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Rating.Count));
        }
    }
}

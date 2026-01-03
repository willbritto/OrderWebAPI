using AutoMapper;
using OrderWebAPI.DTOs.EntitieDTOs;
using OrderWebAPI.Models;

namespace OrderWebAPI.DTOs.Mappings
{
    public class MappingsProfile : Profile
    {

        public MappingsProfile()
        {
            CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
            CreateMap<OrderDTO, OrderModel>().ReverseMap();
        }

    }
}

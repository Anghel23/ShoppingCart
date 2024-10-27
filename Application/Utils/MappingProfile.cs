using Application.DTOs;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;

namespace Application.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap();
            CreateMap<CreateShoppingCartCommand, ShoppingCart>().ReverseMap();
            CreateMap<DeleteShoppingCartCommand, ShoppingCart>().ReverseMap();
            CreateMap<UpdateShoppingCartCommand, ShoppingCart>().ReverseMap();
        }
    }
}

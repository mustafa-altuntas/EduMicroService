using AutoMapper;
using EduMicroService.Catalog.Api.Features.Categories.Dtos;

namespace EduMicroService.Catalog.Api.Features.Categories
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();

        }
    }
}

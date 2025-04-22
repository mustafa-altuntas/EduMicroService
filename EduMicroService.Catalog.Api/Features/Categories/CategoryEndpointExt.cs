using EduMicroService.Catalog.Api.Features.Categories.Create;
using EduMicroService.Catalog.Api.Features.Categories.GetAll;
using EduMicroService.Catalog.Api.Features.Categories.GetById;

namespace EduMicroService.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/categories")
                .WithTags("Categories")
                .CreateCategoryEndpoint()
                .GetAllCategoryEndpointExt()
                .GetCategoryByIdEndpointExt()
                ;
        }
    }
}

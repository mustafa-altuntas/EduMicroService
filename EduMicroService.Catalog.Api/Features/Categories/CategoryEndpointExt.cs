using Asp.Versioning.Builder;
using EduMicroService.Catalog.Api.Features.Categories.Create;
using EduMicroService.Catalog.Api.Features.Categories.GetAll;
using EduMicroService.Catalog.Api.Features.Categories.GetById;

namespace EduMicroService.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/categories")
                .WithTags("Categories")
                .WithApiVersionSet(apiVersionSet)
                .CreateCategoryEndpoint()
                .GetAllCategoryEndpointExt()
                .GetCategoryByIdEndpointExt()
                ;
        }
    }
}

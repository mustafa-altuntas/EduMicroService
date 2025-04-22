using EduMicroService.Catalog.Api.Features.Categories.Dtos;

namespace EduMicroService.Catalog.Api.Features.Courses.Dtos
{
    public record CourseDto(Guid Id, string Name, string Description, decimal Price, string? ImageUrl, CategoryDto category, FeatureDto feature);

}

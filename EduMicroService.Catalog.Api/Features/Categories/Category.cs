using EduMicroService.Catalog.Api.Features.Courses;
using EduMicroService.Catalog.Api.Repositories;

namespace EduMicroService.Catalog.Api.Features.Categories
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = default!;
        public List<Course>? Courses { get; set; }
    }
}

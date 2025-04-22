using EduMicroService.Catalog.Api.Features.Courses.Create;
using EduMicroService.Catalog.Api.Features.Courses.Delete;
using EduMicroService.Catalog.Api.Features.Courses.GetAll;
using EduMicroService.Catalog.Api.Features.Courses.GetById;
using EduMicroService.Catalog.Api.Features.Courses.Update;

namespace EduMicroService.Catalog.Api.Features.Courses
{
    public static class CourseEndpointExt
    {
        public static void AddCourseEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/courses")
                .WithTags("Courses")
                .CreateCourseEndpointExt()
                .GetAllCourseEndpointExt()
                .GetCourseByIdEndpointExt()
                .UpdateCourseEndpointExt()
                .DeleteCourseEndpointExt()
                ;
        }
    }
}

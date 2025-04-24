using Asp.Versioning.Builder;
using EduMicroService.Catalog.Api.Features.Courses.Create;
using EduMicroService.Catalog.Api.Features.Courses.Delete;
using EduMicroService.Catalog.Api.Features.Courses.GetAll;
using EduMicroService.Catalog.Api.Features.Courses.GetAllByUserId;
using EduMicroService.Catalog.Api.Features.Courses.GetById;
using EduMicroService.Catalog.Api.Features.Courses.Update;

namespace EduMicroService.Catalog.Api.Features.Courses
{
    public static class CourseEndpointExt
    {
        public static void AddCourseEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/courses")
                .WithTags("Courses")
                .WithApiVersionSet(apiVersionSet)
                .CreateCourseEndpointExt()
                .GetAllCourseEndpointExt()
                .GetCourseByIdEndpointExt()
                .UpdateCourseEndpointExt()
                .DeleteCourseEndpointExt()
                .GetCoursesByUserIdEndpointExt()
                ;
        }
    }
}

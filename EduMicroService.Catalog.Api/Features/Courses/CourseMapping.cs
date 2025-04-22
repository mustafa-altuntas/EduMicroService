using AutoMapper;
using EduMicroService.Catalog.Api.Features.Courses.Create;
using EduMicroService.Catalog.Api.Features.Courses.Dtos;

namespace EduMicroService.Catalog.Api.Features.Courses
{
    public class CourseMapping : Profile
    {
        public CourseMapping()
        {
            CreateMap<Course, CreateCourseCommand>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();

        }
    }
}

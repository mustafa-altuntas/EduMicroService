using AutoMapper;
using EduMicroService.Catalog.Api.Repositories;
using EduMicroService.Shared;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EduMicroService.Catalog.Api.Features.Courses.Create
{
    public class CreateCourseCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<CreateCourseCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var hasCategory = await context.Categories.AnyAsync(x => x.Id == request.CategoryId, cancellationToken);

            if (!hasCategory)
            {
                return ServiceResult<Guid>.Error("Category not found", $"The Category with id({request.CategoryId}) was not found",HttpStatusCode.NotFound);
            }

            var hasCourse = await context.Courses.AnyAsync(x => x.Name == request.Name, cancellationToken);
            if (hasCourse)
            {
                return ServiceResult<Guid>.Error("Course already exists", $"The Course with name({request.Name}) already exists", HttpStatusCode.Conflict); // 409
            }

            var newCourse = mapper.Map<Course>(request);
            newCourse.Id = NewId.NextSequentialGuid(); // index performance için sequential guid kullanıyoruz
            newCourse.Created = DateTime.UtcNow;
            newCourse.Feature = new Feature
            {
                Duration = 10, // todo: burayı arka planda hesapla
                Rating = 0.0f,
                EducatorFullName = "Ahmet Yılmaz", // todo: get by token payload
            };

            context.Courses.Add(newCourse);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<Guid>.SuccessAsCreated(newCourse.Id,$"/api/course/{newCourse.Id}");
        }
    }
}

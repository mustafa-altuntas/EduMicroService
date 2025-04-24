using AutoMapper;
using EduMicroService.Catalog.Api.Features.Categories;
using EduMicroService.Catalog.Api.Features.Courses.Dtos;
using EduMicroService.Catalog.Api.Repositories;
using EduMicroService.Shared;
using EduMicroService.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EduMicroService.Catalog.Api.Features.Courses.GetById
{
    public record GetCourseByIdQuery(Guid Id) : IRequestByServiceResult<CourseDto>;

    public class GetCourseByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCourseByIdQuery, ServiceResult<CourseDto>>
    {
        public async Task<ServiceResult<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var hasCourse = await context.Courses.FindAsync(request.Id, cancellationToken);
            if (hasCourse is null)
            {
                return ServiceResult<CourseDto>.Error("Course not found", $"The course with Id({request.Id}) was not found", HttpStatusCode.NotFound);
            }
            hasCourse.Category = await context.Categories.FindAsync(hasCourse.CategoryId, cancellationToken) ?? new Category();

            var courseDto = mapper.Map<CourseDto>(hasCourse);
            return ServiceResult<CourseDto>.SuccessAsOk(courseDto);

        }
    }

    public static class GetCourseByIdEndpoint
    {
        public static RouteGroupBuilder GetCourseByIdEndpointExt(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (IMediator meditor, Guid id) =>
            {
                var result = await meditor.Send(new GetCourseByIdQuery(id));
                return result.ToGenericResult();
            })
                .Produces<CourseDto>(StatusCodes.Status200OK)
                .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .WithName("GetCourseById")
                .MapToApiVersion(1, 0);
            return group;
        }
    }
}

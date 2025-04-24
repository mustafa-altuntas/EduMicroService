using AutoMapper;
using EduMicroService.Catalog.Api.Features.Courses.Create;
using EduMicroService.Catalog.Api.Features.Courses.Dtos;
using EduMicroService.Catalog.Api.Features.Courses.GetAll;
using EduMicroService.Catalog.Api.Repositories;
using EduMicroService.Shared;
using EduMicroService.Shared.Extensions;
using EduMicroService.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduMicroService.Catalog.Api.Features.Courses.GetAllByUserId
{

    public record GetCoursesByUserIdQuery(Guid Id) : IRequestByServiceResult<List<CourseDto>>;

    public class GetCoursesByUserIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCoursesByUserIdQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetCoursesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var courses = await context.Courses.Where(x => x.UserId == request.Id).ToListAsync(cancellationToken);
            var categories = await context.Categories.ToListAsync(cancellationToken);
            courses.ForEach(courses =>
            {
                courses.Category = categories.First(x => x.Id == courses.CategoryId);
            });

            var courseDtos = mapper.Map<List<CourseDto>>(courses);
            return ServiceResult<List<CourseDto>>.SuccessAsOk(courseDtos);

        }
    }

    public static class GetCoursesByUserIdEndpoint
    {
        public static RouteGroupBuilder GetCoursesByUserIdEndpointExt(this RouteGroupBuilder group)
        {
            group.MapGet("/user/{userId:guid}", async (IMediator mediator, Guid userId)
                => (await mediator.Send(new GetCoursesByUserIdQuery(userId))).ToGenericResult())
                .WithName("GetByUserIdCourses")
                .MapToApiVersion(1, 0)
                .Produces<List<CourseDto>>(StatusCodes.Status200OK)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .AddEndpointFilter<ValidationFilter<CreateCourseCommand>>();

            return group;
        }
    }




}

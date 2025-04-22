using AutoMapper;
using EduMicroService.Catalog.Api.Features.Courses.Create;
using EduMicroService.Catalog.Api.Features.Courses.Dtos;
using EduMicroService.Catalog.Api.Repositories;
using EduMicroService.Shared;
using EduMicroService.Shared.Extensions;
using EduMicroService.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduMicroService.Catalog.Api.Features.Courses.GetAll
{
    public record GetAllCoursesQuery():IRequestByServiceResult<List<CourseDto>>;

    public class GetAllCoursesQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllCoursesQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await context.Courses.ToListAsync(cancellationToken);
            var categories = await context.Categories.ToListAsync(cancellationToken);
            courses.ForEach(courses =>
            {
                courses.Category = categories.First(x => x.Id == courses.CategoryId);

                // her seferinde veri tabanına gitmek maliyetli
                //courses.Category = await context.Categories.FirstOrDefaultAsync(x => x.Id == courses.CategoryId, cancellationToken);
            });

            var courseDtos = mapper.Map<List<CourseDto>>(courses);
            return ServiceResult<List<CourseDto>>.SuccessAsOk(courseDtos);
        }
    }

    public static class GetAllCoursesEndpoint
    {
        public static RouteGroupBuilder GetAllCourseEndpointExt(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator)
                => (await mediator.Send(new GetAllCoursesQuery())).ToGenericResult())
                .WithName("GetAllCourse")
                .Produces<List<CourseDto>>(StatusCodes.Status200OK)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .AddEndpointFilter<ValidationFilter<CreateCourseCommand>>();

            return group;
        }
    }
}

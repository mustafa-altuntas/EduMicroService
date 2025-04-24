using AutoMapper;
using EduMicroService.Catalog.Api.Features.Categories.Dtos;
using EduMicroService.Catalog.Api.Repositories;
using EduMicroService.Shared;
using EduMicroService.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduMicroService.Catalog.Api.Features.Categories.GetAll
{

    public class GetAllCategoryQuery:IRequestByServiceResult<List<CategoryDto>>;

    public class GetAllCategoryQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
    {
        public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            //var categoryDtos = await context.Categories.Select(x => new CategoryDto(x.Id, x.Name)).ToListAsync(cancellationToken);
            var categoryDtos = mapper.Map<List<CategoryDto>>(await context.Categories.ToListAsync(cancellationToken));
            return ServiceResult<List<CategoryDto>>.SuccessAsOk(categoryDtos);
        }
    }

    public static class GetAllCategoriesEndpoint
    {
        public static RouteGroupBuilder GetAllCategoryEndpointExt(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllCategoryQuery());
                return result.ToGenericResult();
            })
                .WithName("GetAllCategory")
                .MapToApiVersion(1, 0);

            return group;
        }
    }
}

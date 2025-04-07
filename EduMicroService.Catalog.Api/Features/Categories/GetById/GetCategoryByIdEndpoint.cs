using AutoMapper;
using EduMicroService.Catalog.Api.Features.Categories.Dtos;
using EduMicroService.Catalog.Api.Repositories;
using EduMicroService.Shared;
using EduMicroService.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EduMicroService.Catalog.Api.Features.Categories.GetById
{
    public record GetCategoryByIdQuery(Guid Id):IRequest<ServiceResult<CategoryDto>>;

    public class GetCategoryByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, ServiceResult<CategoryDto>>
    {
        public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var hasCategory = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id,cancellationToken);
            if (hasCategory is null)
            {
                return ServiceResult<CategoryDto>.Error("Category not found", $"The category with Id({request.Id}) was not found", HttpStatusCode.NotFound);
            }

            var categoryDto = mapper.Map<CategoryDto>(hasCategory);
            return ServiceResult<CategoryDto>.SuccessAsOk(categoryDto);
        }
    }


    public static class GetCategoryByIdEndpoint
    {
        public static RouteGroupBuilder GetCategoryByIdEndpointExt(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (IMediator mediator,Guid id) =>
            {
                var result = await mediator.Send(new GetCategoryByIdQuery(id));
                return result.ToGenericResult();
            });
            return group;
        }
    }
}

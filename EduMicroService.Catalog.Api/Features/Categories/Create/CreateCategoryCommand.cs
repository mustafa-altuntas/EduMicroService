using EduMicroService.Shared;
using MediatR;

namespace EduMicroService.Catalog.Api.Features.Categories.Create
{
    public record CreateCategoryCommand(string Name) : IRequest<ServiceResult<CreateCategoryResponse>>;
}

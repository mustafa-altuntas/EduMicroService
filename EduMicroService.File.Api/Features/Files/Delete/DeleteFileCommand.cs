using EduMicroService.Shared;

namespace EduMicroService.File.Api.Features.Files.Delete
{
    public record DeleteFileCommand(string FileName) : IRequestByServiceResult;
}

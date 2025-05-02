using EduMicroService.Shared;

namespace EduMicroService.File.Api.Features.Files.Upload
{
    public record UploadFileCommand(IFormFile File) : IRequestByServiceResult<UploadFileCommandResponse>;

}

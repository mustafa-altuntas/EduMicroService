using Asp.Versioning.Builder;
using EduMicroService.File.Api.Features.Files.Delete;
using EduMicroService.File.Api.Features.Files.Upload;

namespace EduMicroService.File.Api.Features
{
    public static class FileEndpointExt
    {
        public static void AddFileEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
        {

            app.MapGroup("/api/v{version:apiVersion}/files")
               .WithTags("Files")
                .WithApiVersionSet(apiVersionSet)
                .UploadFileEndpointExt()
                .DeleteFileEndpointExt()

                ;


        }
    }
}

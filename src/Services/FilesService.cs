using System;
using System.Threading.Tasks;
using LS.Avatars.Api.Attributes;
using Microsoft.AspNetCore.Http;

namespace LS.Avatars.Api.Services;

[Dependency]
public class FileService
{
    private readonly RealFileUploader fileUploader;

    public FileService(RealFileUploader fileUploader)
    {
        this.fileUploader = fileUploader;
    }

    public async Task SaveOriginals(Guid id, IFormFile formFile)
    {
        await fileUploader.CheckFileExists(id);

        await fileUploader.CreateFolder(id);

        await fileUploader.UploadFile(id, formFile);
    }

    public async Task SaveDuplicate(Guid id, string key)
    {
        await fileUploader.CheckFileExists(id);
        await fileUploader.CheckFileExists(id, key);
    }
}
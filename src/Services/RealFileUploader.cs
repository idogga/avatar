using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LS.Avatars.Api.Attributes;
using LS.Avatars.Api.Configurations;
using LS.Avatars.Api.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace LS.Avatars.Api.Services;

[Dependency]
public class RealFileUploader
{
    private readonly RealFileConfiguration pathConfiguration;
    private readonly IWebHostEnvironment appEnvironment;

    public RealFileUploader(
        IOptions<RealFileConfiguration> pathConfiguration,
        IWebHostEnvironment appEnvironment)
    {
        this.pathConfiguration = pathConfiguration.Value;
        this.appEnvironment = appEnvironment;
    }

    public Task CheckFileExists(Guid id, string key)
    {
        if (string.IsNullOrEmpty(pathConfiguration.PathToOriginals))
        {
            throw new BadConfigurationException("File", "Cant't find Path to originals");
        }

        var path = GetFolderPath();
        var fileName = $"{id.ToString()}-{key}";
        if (Directory.GetFiles(path).Any(f => f.Contains(id.ToString())))
        {
            throw new FileExistException();
        }

        return Task.CompletedTask;
    }

    public Task CheckFileExists(Guid id)
    {
        if (string.IsNullOrEmpty(pathConfiguration.PathToOriginals))
        {
            throw new BadConfigurationException("File", "Cant't find Path to originals");
        }

        var path = GetFolderPath();
        if (Directory.GetFiles(path).Any(f => f.Contains(id.ToString())))
        {
            throw new FileExistException();
        }

        return Task.CompletedTask;
    }

    public Task CreateFolder(Guid id)
    {
        Directory.CreateDirectory(GetFolderPath());
        return Task.CompletedTask;
    }

    public async Task UploadFile(Guid id, IFormFile formFile)
    {
        var path = GetPath(id);
        var extension = Path.GetExtension(formFile.FileName);
        if (string.IsNullOrEmpty(extension))
        {
            throw new BadConfigurationException("File", "File should have an extension!");
        }

        var a = path + "." + extension;
        using var fileStream = new FileStream(a, FileMode.Create);
        await formFile.CopyToAsync(fileStream);
    }

    private string GetPath(Guid id)
    {
        return Path.Combine(GetFolderPath(), id.ToString());
    }

    private string GetFolderPath()
    {
        return Path.Combine(appEnvironment.ContentRootPath, pathConfiguration.PathToOriginals);
    }
}
using System;
using System.Threading.Tasks;
using LS.Avatars.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LS.Avatars.Api;

[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
    [HttpPost("{id}")]
    public async Task Save(
        Guid id,
        [FromForm] A formFile,
        [FromServices] FileService fileService)
    {
        await fileService.SaveOriginals(id, formFile.FormFile);
    }

    public record A
    {
        public IFormFile FormFile { get; init; }

        public string? Ab { get; set; }
    }
}

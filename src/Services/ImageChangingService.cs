using System;
using System.Threading.Tasks;
using Aspose.Imaging;
using LS.Avatars.Api.Attributes;
using LS.Avatars.Api.Configurations;

namespace LS.Avatars.Api.Services;

[Dependency]
public class ImageChangingService
{
    private readonly AvatarsConfiguration avatarsConfiguration;

    public ImageChangingService(AvatarsConfiguration avatarsConfiguration)
    {
        this.avatarsConfiguration = avatarsConfiguration;
    }

    public async Task ChangeImage(Guid id, ImagesController.A formFile)
    {
        foreach (var dimension in avatarsConfiguration.Dimensions)
        {
            using var image = Image.Load(formFile.FormFile.OpenReadStream());
            image.Resize(dimension.Width, dimension.Height);
            image.Save();
        }
    }
}
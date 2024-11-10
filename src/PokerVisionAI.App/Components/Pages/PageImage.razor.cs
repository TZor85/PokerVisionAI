using PokerVisionAI.Domain.Dtos;
using PokerVisionAI.Domain.Helpers;
using PokerVisionAI.Features.Images.Create;
using PokerVisionAI.Features.Images.Delete;

namespace PokerVisionAI.App.Components.Pages
{
    public partial class PageImage
    {
        private IEnumerable<ImageDTO>? images;
        private ImageDTO? newImage = new() { Name = "" };
        private ImageDTO? selectedImage;
        private int Count = 0;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            var imgs = await _useCases.ListImages.ExecuteAsync();
            images = imgs.Value;
            Count = images.Count();
        }

        private async Task AddImage()
        {
            if (string.IsNullOrWhiteSpace(newImage?.Name))
                return;

            var image = new ImageDTO
            {
                Name = newImage.Name,
                Force = newImage.Force,
                Suit = newImage.Suit,
                BinaryValue = newImage.BinaryValue,
                ImageEncrypted = newImage.ImageEncrypted,
                ImageBase64 = EncrypterImageHelper.GetImageBase64Decrypted(
                                            newImage?.ImageEncrypted ?? string.Empty,
                                            _encrypterOptions?.Value.Key ?? string.Empty)
            };

            var request = new CreateImageRequest(
                                        image?.Name ?? string.Empty,
                                        image?.ImageEncrypted ?? string.Empty,
                                        image?.ImageBase64 ?? string.Empty,
                                        image?.BinaryValue ?? string.Empty,
                                        image?.Force ?? 0,
                                        image?.Suit ?? 0);

            await _useCases.CreateImage.ExecuteAsync(request);
            await LoadData();

            // Reiniciar el formulario
            newImage = new() { Name = "" };
        }

        private async Task DeleteImage(ImageDTO image)
        {
            await _useCases.DeleteImage.ExecuteAsync(new DeleteImageRequest(image.Name));
            images?.ToList().Remove(image);
        }
    }
}

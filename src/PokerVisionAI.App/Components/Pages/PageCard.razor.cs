using PokerVisionAI.Domain.Dtos;
using PokerVisionAI.Domain.Helpers;
using PokerVisionAI.Features.Card.Create;
using PokerVisionAI.Features.Card.Delete;

namespace PokerVisionAI.App.Components.Pages
{
    public partial class PageCard
    {
        private IEnumerable<CardDTO>? images;
        private CardDTO? newImage = new() { Name = "" };
        private CardDTO? selectedImage;
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

            var image = new CardDTO
            {
                Name = newImage.Name,
                Force = newImage.Force,
                Suit = newImage.Suit,
                BinaryValue = newImage.BinaryValue,
                ImageBase64 = EncrypterImageHelper.GetImageBase64Decrypted(string.Empty,
                                            _encrypterOptions?.Value.Key ?? string.Empty)
            };

            var request = new CreateCardRequest(
                                        image?.Name ?? string.Empty,
                                        string.Empty,
                                        image?.ImageBase64 ?? string.Empty,
                                        image?.BinaryValue ?? string.Empty,
                                        image?.Force ?? 0,
                                        image?.Suit ?? 0);

            await _useCases.CreateImage.ExecuteAsync(request);
            await LoadData();

            // Reiniciar el formulario
            newImage = new() { Name = "" };
        }

        private async Task DeleteImage(CardDTO image)
        {
            await _useCases.DeleteImage.ExecuteAsync(new DeleteCardRequest(image.Name));
            images?.ToList().Remove(image);
        }
    }
}

using Microsoft.AspNetCore.Components.Forms;
using SkiaSharp;

namespace PokerVisionAI.App.Services;

public class ImageCropperService
{
    public async Task<string> CropImageToBase64(IBrowserFile file, int x, int y, int width, int height)
    {
        try
        {
            using var ms = new MemoryStream();
            await file.OpenReadStream(maxAllowedSize: 20 * 1024 * 1024).CopyToAsync(ms);
            ms.Position = 0;

            // Cargar imagen con SkiaSharp
            using var originalBitmap = SKBitmap.Decode(ms);

            // Crear un nuevo bitmap para la región recortada
            using var croppedBitmap = new SKBitmap(width, height);

            // Crear un canvas para dibujar
            using var canvas = new SKCanvas(croppedBitmap);

            // Definir el área a recortar
            var sourceRect = new SKRectI(x, y, x + width, y + height);
            var destRect = new SKRectI(0, 0, width, height);

            // Dibujar la región recortada
            canvas.DrawBitmap(originalBitmap, sourceRect, destRect);

            // Convertir a base64
            using var image = SKImage.FromBitmap(croppedBitmap);
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            var croppedMs = new MemoryStream();
            data.SaveTo(croppedMs);

            return $"data:image/png;base64,{Convert.ToBase64String(croppedMs.ToArray())}";
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al recortar la imagen: {ex.Message}");
        }
    }
}

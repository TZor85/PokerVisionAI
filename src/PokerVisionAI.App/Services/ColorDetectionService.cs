using Microsoft.AspNetCore.Components.Forms;
using SkiaSharp;

namespace PokerVisionAI.App.Services;

public class ColorDetectionService
{
    public async Task<SKColor> GetPixelColor(IBrowserFile file, int x, int y)
    {
        try
        {
            using var ms = new MemoryStream();
            await file.OpenReadStream(maxAllowedSize: 20 * 1024 * 1024).CopyToAsync(ms);
            ms.Position = 0;

            // Cargar imagen
            using var bitmap = SKBitmap.Decode(ms);

            // Obtener el color del pixel en la posición x,y
            SKColor pixelColor = bitmap.GetPixel(x, y);

            return pixelColor;

            // Si quieres los valores individuales:
            // byte red = pixelColor.Red;
            // byte green = pixelColor.Green;
            // byte blue = pixelColor.Blue;
            // byte alpha = pixelColor.Alpha;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener el color: {ex.Message}");
        }
    }

}

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace PokerVisionAI.App.Services
{
    public class FileProcessorService
    {
        private readonly int _maxFileSize;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public FileProcessorService(int maxFileSize = 20 * 1024 * 1024) // 20MB por defecto
        {
            _maxFileSize = maxFileSize;
        }

        public async Task<(string Error, string? ImageDataUrl)> ProcessFileAsync(IBrowserFile file)
        {
            if (file == null)
                return ("No se ha seleccionado ningún archivo.", null);

            try
            {
                await _semaphore.WaitAsync();

                if (file.Size > _maxFileSize)
                    return ("El archivo excede el tamaño máximo permitido.", null);

                using var streamRef = new DotNetStreamReference(await ConvertFileToMemoryStream(file));
                var base64 = await ConvertStreamToBase64(streamRef.Stream);

                return (null!, $"data:{file.ContentType};base64,{base64}");
            }
            catch (Exception ex)
            {
                return ($"Error al procesar el archivo: {ex.Message}", null);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task<MemoryStream> ConvertFileToMemoryStream(IBrowserFile file)
        {
            var ms = new MemoryStream();
            using var stream = file.OpenReadStream(_maxFileSize);

            var buffer = new byte[16 * 1024]; // 16KB buffer
            int read;
            while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await ms.WriteAsync(buffer, 0, read);
            }

            ms.Position = 0;
            return ms;
        }

        private async Task<string> ConvertStreamToBase64(Stream stream)
        {
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}

using PokerVisionAI.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokerVisionAI.Features.InitialConfig.Action
{
    public class GetAllActions
    {
        public async Task<ActionDTO?>? ExecuteAsync()
        {
            try
            {
                var customFileType = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                { DevicePlatform.WinUI, new[] { ".json" } },
                { DevicePlatform.iOS, new[] { "public.json" } },
                { DevicePlatform.Android, new[] { "application/json" } },
                { DevicePlatform.macOS, new[] { "json" } }
                    });

                var options = new PickOptions
                {
                    PickerTitle = "Por favor seleccione un archivo JSON",
                    FileTypes = customFileType
                };

                var result = await FilePicker.Default.PickAsync(options);

                if (result != null)
                {
                    if (result.FileName.EndsWith("json", StringComparison.OrdinalIgnoreCase))
                    {
                        // Leer el contenido del archivo
                        using var stream = await result.OpenReadAsync();
                        using var reader = new StreamReader(stream);
                        var jsonContent = await reader.ReadToEndAsync();

                        // Deserializar el JSON a una lista de ActionDTO
                        var action = JsonSerializer.Deserialize<ActionDTO>(jsonContent, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        return action;
                    }
                    else
                    {
                        if (Application.Current?.MainPage != null)
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", "Por favor seleccione un archivo JSON válido", "OK");
                        }
                        return null;
                    }
                }
                else
                {
                    // Usuario canceló la selección
                    return null;
                }
            }
            catch (Exception ex)
            {
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar el archivo: {ex.Message}", "OK");
                }
                return null;
            }
        } 
    }
}

using PokerVisionAI.Domain.Dtos;
using PokerVisionAI.Features.Regions.Create;
using PokerVisionAI.Features.Regions.Update;

namespace PokerVisionAI.App.Components.Pages
{
    public partial class PageRegion
    {
        private IEnumerable<RegionDTO>? regions;
        private RegionDTO newRegion = new() { Name = "" };
        private string? colorHex;
        private int Count = 0;
        private bool update = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            var locRegions = await _useCases.ListRegions.ExecuteAsync();
            regions = locRegions.Value.Where(w => w.DeletedDate == null);
            Count = regions.Count();
        }

        private async Task AddRegion()
        {
            if (string.IsNullOrWhiteSpace(newRegion.Name))
            {
                return;
            }

            if (update)
            {
                var requestUpdate = new UpdateRegionRequest(newRegion.Name,
                    newRegion.PosX,
                    newRegion.PosY,
                    newRegion.Width,
                    newRegion.Height,
                    newRegion.IsHash,
                    newRegion.IsColor,
                    newRegion.IsBoard,
                    newRegion.Color
                );

                await _useCases.UpdateRegion.ExecuteAsync(requestUpdate);
                update = false;
            }
            else
            {
                var request = new CreateRegionRequest(newRegion.Name,
                    newRegion.PosX,
                    newRegion.PosY,
                    newRegion.Width,
                    newRegion.Height,
                    newRegion.IsHash,
                    newRegion.IsColor,
                    newRegion.IsBoard,
                    newRegion.Color
                );

                await _useCases.CreateRegion.ExecuteAsync(request);
            }

            await LoadData();

            // Reiniciar el formulario
            newRegion = new() { Name = "" };
            colorHex = null;
        }

        private void DeleteRegion(RegionDTO region)
        {
            regions?.ToList().Remove(region);
        }

        private void UpdateRegion(RegionDTO region)
        {
            newRegion = region;
            colorHex = region.Color;
            update = true;
        }

        private void OnColorHexChange(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                newRegion.Color = null;
                return;
            }

            // Asegurar que el valor comience con #
            if (!value.StartsWith("#"))
            {
                value = "#" + value;
            }

            // Validar el formato hexadecimal
            if (System.Text.RegularExpressions.Regex.IsMatch(value, "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$"))
            {
                newRegion.Color = value;
                colorHex = value;
            }
        }

        private void OnColorPickerChange(string? value)
        {
            colorHex = value;
        }
    }
}

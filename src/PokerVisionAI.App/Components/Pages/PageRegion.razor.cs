using PokerVisionAI.Domain.Dtos;

namespace PokerVisionAI.App.Components.Pages
{
    public partial class PageRegion
    {
        private List<RegionCategoryDTO>? regionsCategories = new List<RegionCategoryDTO>();
        private List<Domain.ValueObjects.Region>? regions;
        private int Count = 0;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            var locRegions = await _useCases.ListRegions.ExecuteAsync();
            foreach (var item in locRegions.Value.ToList())
            {
                var dto = new RegionCategoryDTO { Name = item.Name, Regions = item.Regions };
                regionsCategories!.Add(dto);
            }

            Count = regionsCategories.SelectMany(s => s.Regions).Count();
            regions = regionsCategories.SelectMany(s => s.Regions).ToList();
        }
    }
}

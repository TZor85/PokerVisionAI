using PokerVisionAI.Domain.Dtos;

namespace PokerVisionAI.App.Components.Pages;

public partial class PageTableMap
{
    private TableMapDTO selectedTableMap = new()
        {
            Name = "",
            Regions = new()
        };

    private List<TableMapDTO> tableMaps = new();
    private List<string> availableNames = new();
    private List<RegionCategoryDTO> allRegions = new();

    private List<RegionCategoryDTO> availableRegionsList = new();


    protected override async Task OnInitializedAsync()
    {
        foreach (var item in _options.Value.PokerTable)
        {
            availableNames.Add(string.Join(" ",
                            item.Split('_')
                            .Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower())));
        }

        await LoadData();

        availableRegionsList = allRegions.Where(r => selectedTableMap.Regions == null || !selectedTableMap.Regions.Any(sr => sr.Name == r.Name)).ToList();
    }

    private async Task LoadData()
    {
        // Cargar TableMaps existentes
        tableMaps = await _useCasesTableMaps.ListTableMaps.ExecuteAsync();
    }

    private void AddRegion(RegionCategoryDTO region)
    {
        if (selectedTableMap.Regions == null)
            selectedTableMap.Regions = new List<RegionCategoryDTO>();

        if (!selectedTableMap.Regions.Any(r => r.Name == region.Name))
        {
            availableRegionsList.Remove(region);
            selectedTableMap.Regions.Add(region);            
        }

        StateHasChanged();
    }

    private void RemoveRegion(RegionCategoryDTO region)
    {
        availableRegionsList.Add(region);
        selectedTableMap.Regions?.Remove(region);
        StateHasChanged();
    }

    private void EditTableMap(TableMapDTO tableMap)
    {
        selectedTableMap = new TableMapDTO
            {
                Name = tableMap.Name,
                Regions = tableMap.Regions?.ToList() ?? new()
            };
    }

    private async Task DeleteTableMap(TableMapDTO tableMap)
    {
        // Aquí eliminarías de tu base de datos
        // await YourService.DeleteTableMap(tableMap);

        // Recargar datos
        await LoadData();
    }
}
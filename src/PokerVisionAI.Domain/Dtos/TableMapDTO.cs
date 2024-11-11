namespace PokerVisionAI.Domain.Dtos;

public class TableMapDTO
{
    public required string Name { get; set; }
    public List<RegionDTO>? Regions { get; set; }
}

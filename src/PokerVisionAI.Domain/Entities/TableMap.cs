namespace PokerVisionAI.Domain.Entities;

public class TableMap
{
    public required string Id { get; set; }
    public List<Region>? Regions { get; set; } = [];
}

namespace PokerVisionAI.Domain.Dtos;

public class RegionDTO
{
    public required string Name { get; set; }
    public int PosX { get; set; }
    public int PosY { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool IsHash { get; set; }
    public bool IsColor { get; set; }
    public bool IsBoard { get; set; }
    public string? Color { get; set; }
    public DateTime? DeletedDate { get; set; } = null;
}

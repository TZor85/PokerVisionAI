namespace PokerVisionAI.Domain.Entities;

public class Image
{
    public required string Id { get; set; }
    public Microsoft.Maui.Controls.Image? ValueImage { get; set; }
    public string? BinaryValue { get; set; }
    public int Force { get; set; }
    public int Suit { get; set; }
}

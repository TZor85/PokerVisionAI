namespace PokerVisionAI.Features.Regions.Update;

public record UpdateRegionRequest(string Name, int PosX, int PosY, int Width, int Height, bool IsHash, bool IsColor, bool IsBoard, string? Color);


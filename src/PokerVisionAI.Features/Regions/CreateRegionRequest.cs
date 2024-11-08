namespace PokerVisionAI.Features.Regions;

public record CreateRegionRequest(string Name, int PosX, int PosY, int Width, int Height, bool IsHash, bool IsColor, bool IsBoard, string? Color);


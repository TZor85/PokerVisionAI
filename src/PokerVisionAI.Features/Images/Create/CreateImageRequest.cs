namespace PokerVisionAI.Features.Images.Create;

public record CreateImageRequest(string Name, Image Image, string BinaryValue, int Force, int Suit);


namespace PokerVisionAI.Features.Images.Create;

public record CreateImageRequest(string Name, string ImageEncrypted, string ImageBase64, string BinaryValue, int Force, int Suit);


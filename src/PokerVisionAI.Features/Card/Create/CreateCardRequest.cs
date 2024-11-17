namespace PokerVisionAI.Features.Card.Create;

public record CreateCardRequest(string Name, string ImageEncrypted, string ImageBase64, string BinaryValue, int Force, int Suit);


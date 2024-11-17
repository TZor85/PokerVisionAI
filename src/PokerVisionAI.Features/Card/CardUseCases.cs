using PokerVisionAI.Features.Card.Create;
using PokerVisionAI.Features.Card.CreateAll;
using PokerVisionAI.Features.Card.Delete;
using PokerVisionAI.Features.Card.Get;
using PokerVisionAI.Features.Card.List;

namespace PokerVisionAI.Features.Card;

public record CardUseCases(CreateCard CreateImage, GetCard GetImage, ListCards ListImages, DeleteCard DeleteImage, CreateAllCards CreateAllCards);


using PokerVisionAI.Features.Card.CreateAll;
using PokerVisionAI.Features.Images.Create;
using PokerVisionAI.Features.Images.Delete;
using PokerVisionAI.Features.Images.Get;
using PokerVisionAI.Features.Images.List;

namespace PokerVisionAI.Features.Images;

public record CardUseCases(CreateCard CreateImage, GetCard GetImage, ListCards ListImages, DeleteCard DeleteImage, CreateAllCards CreateAllCards);


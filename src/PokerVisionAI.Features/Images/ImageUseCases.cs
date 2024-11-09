using PokerVisionAI.Features.Images.Create;
using PokerVisionAI.Features.Images.Get;
using PokerVisionAI.Features.Images.List;
using PokerVisionAI.Features.Images.Update;

namespace PokerVisionAI.Features.Images;

public record ImageUseCases(CreateImage CreateImage, GetImage GetImage, ListImages ListImages, UpdateImage UpdateImage);


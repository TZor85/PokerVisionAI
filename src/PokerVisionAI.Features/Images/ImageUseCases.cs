using PokerVisionAI.Features.Images.Create;
using PokerVisionAI.Features.Images.Delete;
using PokerVisionAI.Features.Images.Get;
using PokerVisionAI.Features.Images.List;

namespace PokerVisionAI.Features.Images;

public record ImageUseCases(CreateImage CreateImage, GetImage GetImage, ListImages ListImages, DeleteImage DeleteImage);


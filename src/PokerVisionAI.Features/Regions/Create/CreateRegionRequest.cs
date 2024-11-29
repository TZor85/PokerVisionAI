namespace PokerVisionAI.Features.Regions.Create;

public record CreateRegionRequest(string Name, List<Domain.ValueObjects.Region> Regions);


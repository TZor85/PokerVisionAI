namespace PokerVisionAI.Features.TableMap.Create;

public record CreateTableMapRequest(string Name, List<Domain.Entities.RegionCategory> Regions);


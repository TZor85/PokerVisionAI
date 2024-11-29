using Ardalis.Result;
using Marten;

namespace PokerVisionAI.Features.Regions.Create;

public class CreateRegion
{
    readonly IDocumentStore _documentStore;

    public CreateRegion(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Result> ExecuteAsync(CreateRegionRequest request, CancellationToken ct = default)
    {
        try
        {
            var region = new Domain.Entities.RegionCategory
            {
                Id = request.Name,
                Regions = request.Regions
            };

            using var session = _documentStore.LightweightSession();
            var existingRegion = await session.LoadAsync<Domain.Entities.RegionCategory>(region.Id, ct);

            if (existingRegion != null)
                return Result.Conflict($"Region with name {region.Id} already exists.");
            
            session.Store(region);
            await session.SaveChangesAsync(ct);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }
    }
}

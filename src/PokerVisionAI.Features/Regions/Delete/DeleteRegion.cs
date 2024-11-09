using Ardalis.Result;
using Marten;

namespace PokerVisionAI.Features.Regions.Delete;

public class DeleteRegion
{
    readonly IDocumentStore _documentStore;

    public DeleteRegion(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Result> ExecuteAsync(DeleteRegionRequest request, CancellationToken ct = default)
    {
        try
        {
            using var session = _documentStore.LightweightSession();
            var region = await session.LoadAsync<Domain.Entities.Region>(request.Name, ct);
            if (region == null)
                return Result.NotFound();

            if (region.DeletedDate != null)
                return Result.Success();

            region.DeletedDate = DateTime.UtcNow;
            session.Delete(region);
            await session.SaveChangesAsync(ct);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }
    }
}

using Ardalis.Result;
using Marten;

namespace PokerVisionAI.Features.TableMap.Delete;

public class DeleteTableMap
{
    readonly IDocumentStore _documentStore;

    public DeleteTableMap(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Result> ExecuteAsync(DeleteTableMapRequest request, CancellationToken ct = default)
    {
        try
        {
            using var session = _documentStore.LightweightSession();
            var tableMap = await session.LoadAsync<Domain.Entities.TableMap>(request.Name, ct);

            if (tableMap == null)
                return Result.NotFound($"TableMap with name {request.Name} not found.");

            session.Delete(tableMap);
            await session.SaveChangesAsync(ct);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }
    }
}

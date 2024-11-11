using Ardalis.Result;
using Marten;

namespace PokerVisionAI.Features.TableMap.CreateAll;

public class CreateAllTableMap
{
    readonly IDocumentStore _documentStore;

    public CreateAllTableMap(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Result> ExecuteAsync(CreateAllTableMapRequest request, CancellationToken ct = default)
    {
        try
        {
            using var session = _documentStore.LightweightSession();
            foreach (var tableMap in request.TableMaps)
            {
                session.Store(tableMap);
            }
            await session.SaveChangesAsync(ct);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }
    }
}

using Ardalis.Result;
using Marten;

namespace PokerVisionAI.Features.TableMap.Create;

public class CreateTableMap
{
    readonly IDocumentStore _documentStore;

    public CreateTableMap(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Result> ExecuteAsync(CreateTableMapRequest request, CancellationToken ct = default)
    {
        try
        {
            var tableMap = new Domain.Entities.TableMap
            {
                Id = request.Name,
                Regions = request.Regions
            };

            using var session = _documentStore.LightweightSession();
            var existingTableMap = await session.LoadAsync<Domain.Entities.TableMap>(tableMap.Id, ct);

            if (existingTableMap != null)
                return Result.Conflict($"TableMap with name {tableMap.Id} already exists.");

            session.Store(tableMap);
            await session.SaveChangesAsync(ct);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }
    }
}

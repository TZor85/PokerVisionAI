using Ardalis.Result;
using Marten;
using PokerVisionAI.Domain.Dtos;
using PokerVisionAI.Domain.Mappers;

namespace PokerVisionAI.Features.TableMap.Get;

public class GetTableMap
{
    readonly IDocumentStore _documentStore;

    public GetTableMap(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Result<TableMapDTO>> ExecuteAsync(string name, CancellationToken ct = default)
    {
        try
        {
            using var session = _documentStore.QuerySession();
            var tableMap = await session.Query<Domain.Entities.TableMap>().FirstOrDefaultAsync(x => x.Id == name, ct);

            if (tableMap == null)
                return Result<TableMapDTO>.NotFound($"TableMap with name {name} not found.");

            return Result<TableMapDTO>.Success(tableMap.ToDto());
        }
        catch (Exception ex)
        {
            return Result<TableMapDTO>.CriticalError(ex.Message);
        }
    }
}

using Ardalis.Result;
using Marten;
using Marten.Pagination;
using PokerVisionAI.Domain.Dtos;
using PokerVisionAI.Domain.Mappers;

namespace PokerVisionAI.Features.TableMap.List;

public class ListTableMaps
{
    readonly IDocumentStore _documentStore;

    public ListTableMaps(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<PagedResult<List<TableMapDTO>>> ExecuteAsync(int pageNumber = 1, int pageSize = 50, CancellationToken ct = default)
    {
        try
        {
            using var session = _documentStore.QuerySession();
            var tMap = await session.Query<Domain.Entities.TableMap>().ToPagedListAsync(pageNumber, pageSize, ct);

            var pageInfo = new PagedInfo(tMap.PageNumber, tMap.PageSize, tMap.PageCount, tMap.TotalItemCount);
            var tableMapsDto = tMap.Select(t => t.ToDto()).ToList();

            return Result.Success(tableMapsDto).ToPagedResult(pageInfo);
        }
        catch (Exception ex)
        {
            return Result<List<TableMapDTO>>.CriticalError(ex.Message).ToPagedResult(new PagedInfo(0, 0, 0, 0));
        }
    }
}

using Ardalis.Result;
using Marten;
using Marten.Pagination;
using PokerVisionAI.Domain.Dtos;
using PokerVisionAI.Domain.Mappers;

namespace PokerVisionAI.Features.Regions.List;

public class ListRegions
{
    readonly IDocumentStore _documentStore;

    public ListRegions(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<PagedResult<List<RegionCategoryDTO>>> ExecuteAsync(int pageNumber = 1, int pageSize = 50, CancellationToken ct = default)
    {
        try
        {
            using var session = _documentStore.QuerySession();
            var regions = await session.Query<Domain.Entities.RegionCategory>().ToPagedListAsync(pageNumber, pageSize, ct);

            var pageInfo = new PagedInfo(regions.PageNumber, regions.PageSize, regions.PageCount, regions.TotalItemCount);
            var catsDto = regions.Select(t => t.ToDto()).ToList();

            return Result.Success(catsDto).ToPagedResult(pageInfo);
        }
        catch (Exception ex)
        {
            return Result<List<RegionCategoryDTO>>.CriticalError(ex.Message).ToPagedResult(new PagedInfo(0, 0, 0, 0));
        }
    }
}

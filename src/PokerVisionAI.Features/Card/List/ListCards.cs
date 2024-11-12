using Ardalis.Result;
using Marten;
using Marten.Pagination;
using PokerVisionAI.Domain.Dtos;
using PokerVisionAI.Domain.Mappers;

namespace PokerVisionAI.Features.Images.List;

public class ListCards
{
    private readonly IDocumentStore _documentStore;

    public ListCards(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Result<List<CardDTO>>> ExecuteAsync(int pageNumber = 1, int pageSize = 100, CancellationToken ct = default)
    {
        try
        {
            using var session = _documentStore.QuerySession();
            var images = await session.Query<Domain.Entities.Card>().ToPagedListAsync(pageNumber, pageSize, ct);

            var pageInfo = new PagedInfo(images.PageNumber, images.PageSize, images.PageCount, images.TotalItemCount);
            var imageDto = images.Select(t => t.ToDto()).ToList();

            return Result.Success(imageDto).ToPagedResult(pageInfo);
        }
        catch (Exception ex)
        {
            return Result<List<CardDTO>>.CriticalError(ex.Message).ToPagedResult(new PagedInfo(0, 0, 0, 0));
        }
    }
}

using Ardalis.Result;
using Marten;
using Marten.Pagination;
using PokerVisionAI.Domain.Dtos;
using PokerVisionAI.Domain.Mappers;

namespace PokerVisionAI.Features.Images.List;

public class ListImages
{
    private readonly IDocumentStore _documentStore;

    public ListImages(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<PagedResult<List<ImageDTO>>> ExecuteAsync(int pageNumber = 1, int pageSize = 100, CancellationToken ct = default)
    {
        try
        {
            using var session = _documentStore.QuerySession();
            var images = await session.Query<Domain.Entities.Image>().ToPagedListAsync(pageNumber, pageSize, ct);

            var pageInfo = new PagedInfo(images.PageNumber, images.PageSize, images.PageCount, images.TotalItemCount);
            var imageDto = images.Select(t => t.ToDto()).ToList();

            return Result.Success(imageDto).ToPagedResult(pageInfo);
        }
        catch (Exception ex)
        {
            return Result<List<ImageDTO>>.CriticalError(ex.Message).ToPagedResult(new PagedInfo(0, 0, 0, 0));
        }
    }
}

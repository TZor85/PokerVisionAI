using Marten;
using Ardalis.Result;
using PokerVisionAI.Domain.Dtos;
using PokerVisionAI.Domain.Mappers;

namespace PokerVisionAI.Features.Images.Get;

public class GetCard
{
    private readonly IDocumentStore _documentStore;

    public GetCard(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Result<CardDTO>> ExecuteAsync(string id, CancellationToken ct = default)
    {
        try
        {
            using var session = _documentStore.QuerySession();
            var img = await session.LoadAsync<Domain.Entities.Card>(id, ct);
            if (img == null)
                return Result.NotFound();
            return Result.Success(img.ToDto());
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }
    }
}

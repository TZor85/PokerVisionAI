using Ardalis.Result;
using Marten;
using PokerVisionAI.Domain.Dtos;
using PokerVisionAI.Domain.Mappers;

namespace PokerVisionAI.Features.Regions.Get;

public class GetRegion
{
    private readonly IDocumentStore _documentStore;

    public GetRegion(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Result<RegionDTO>> ExecuteAsync(string id, CancellationToken ct = default)
    {
        try
        {
            using var session = _documentStore.QuerySession();
            var reg = await session.LoadAsync<Domain.Entities.Region>(id, ct);
            if (reg == null)
                return Result.NotFound();
            return Result.Success(reg.ToDto());
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }
    }

}

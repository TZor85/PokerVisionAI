using Ardalis.Result;
using Marten;

namespace PokerVisionAI.Features.Images.Delete;

public class DeleteCard
{
    readonly IDocumentStore _documentStore;

    public DeleteCard(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Result> ExecuteAsync(DeleteCardRequest request, CancellationToken ct = default)
    {
        try
        {
            using var session = _documentStore.LightweightSession();
            var image = await session.LoadAsync<Domain.Entities.Card>(request.Name, ct);
            if (image == null)
                return Result.NotFound();


            session.Delete(image);
            await session.SaveChangesAsync(ct);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }
    }
}

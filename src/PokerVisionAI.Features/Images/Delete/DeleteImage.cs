using Ardalis.Result;
using Marten;

namespace PokerVisionAI.Features.Images.Delete;

public class DeleteImage
{
    readonly IDocumentStore _documentStore;

    public DeleteImage(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Result> ExecuteAsync(DeleteImageRequest request, CancellationToken ct = default)
    {
        try
        {
            using var session = _documentStore.LightweightSession();
            var image = await session.LoadAsync<Domain.Entities.Image>(request.Name, ct);
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

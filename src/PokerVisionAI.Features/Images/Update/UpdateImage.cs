using Ardalis.Result;
using Marten;

namespace PokerVisionAI.Features.Images.Update;

public class UpdateImage
{
    readonly IDocumentStore _documentStore;

    public UpdateImage(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Result> ExecuteAsync(UpdateImageRequest request, CancellationToken ct = default)
    {
        try
        {
            using var session = _documentStore.LightweightSession();
            var image = await session.LoadAsync<Domain.Entities.Image>(request.Name, ct);
            if (image == null)
                return Result.NotFound();

            image.Force = request.Force;
            image.Suit = request.Suit;
            image.BinaryValue = request.BinaryValue;

            session.Store(image);
            await session.SaveChangesAsync(ct);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }
    }
}

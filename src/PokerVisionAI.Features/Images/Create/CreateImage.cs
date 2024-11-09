using Ardalis.Result;
using Marten;

namespace PokerVisionAI.Features.Images.Create;

public class CreateImage
{
    readonly IDocumentStore _documentStore;

    public CreateImage(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Result> ExecuteAsync(CreateImageRequest request, CancellationToken ct = default)
    {
        try
        {
            var image = new Domain.Entities.Image
            {
                Id = request.Name,
                ValueImage = request.Image,
                BinaryValue = request.BinaryValue,
                Force = request.Force,
                Suit = request.Suit
            };

            using var session = _documentStore.LightweightSession();
            var existingImage = await session.LoadAsync<Domain.Entities.Image>(image.Id, ct);

            if (existingImage != null)
                return Result.Conflict($"Image with name {image.Id} already exists.");

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

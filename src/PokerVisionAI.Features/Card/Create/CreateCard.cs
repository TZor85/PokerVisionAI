using Ardalis.Result;
using Marten;

namespace PokerVisionAI.Features.Images.Create;

public class CreateCard
{
    readonly IDocumentStore _documentStore;

    public CreateCard(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Result> ExecuteAsync(CreateCardRequest request, CancellationToken ct = default)
    {
        try
        {
            var image = new Domain.Entities.Card
            {
                Id = request.Name,
                ImageEncrypted = request.ImageEncrypted,
                BinaryValue = request.BinaryValue,
                ImageBase64 = request.ImageBase64,
                Force = request.Force,
                Suit = request.Suit
            };

            using var session = _documentStore.LightweightSession();
            var existingImage = await session.LoadAsync<Domain.Entities.Card>(image.Id, ct);

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

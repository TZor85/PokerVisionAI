using Ardalis.Result;
using Marten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerVisionAI.Features.Regions.Update;

public class UpdateRegion
{
    readonly IDocumentStore _documentStore;

    public UpdateRegion(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Result> ExecuteAsync(UpdateRegionRequest request, CancellationToken ct = default)
    {
        try
        {
            using var session = _documentStore.LightweightSession();
            var region = await session.LoadAsync<Domain.Entities.RegionCategory>(request.Category, ct);
            if (region == null)
                return Result.NotFound();

            var regionToRemove = region.Regions?.FirstOrDefault(r => r.Name == request.Name);
            if (regionToRemove != null)
            {
                region.Regions?.Remove(regionToRemove);
            }

            var regionCategory = new Domain.ValueObjects.Region(request.Category, request.Name, request.PosX, request.PosY, request.Width, request.Height, request.IsHash, request.IsColor, request.IsBoard, request.Color, request.IsOnlyNumber, request.InactiveUmbral, request.Umbral);

            region.Regions?.Add(regionCategory);

            session.Store(region);
            await session.SaveChangesAsync(ct);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }
    }
}

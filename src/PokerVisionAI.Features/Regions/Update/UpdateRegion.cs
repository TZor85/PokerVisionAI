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
            var region = await session.LoadAsync<Domain.Entities.Region>(request.Name, ct);
            if (region == null)
                return Result.NotFound();

            region.PosX = request.PosX;
            region.PosY = request.PosY;
            region.Width = request.Width;
            region.Height = request.Height;
            region.IsHash = request.IsHash;
            region.IsColor = request.IsColor;
            region.IsBoard = request.IsBoard;
            region.Color = request.Color;

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

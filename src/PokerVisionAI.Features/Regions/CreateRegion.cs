using Marten;
using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerVisionAI.Features.Regions;

public class CreateRegion
{
    readonly IDocumentStore _documentStore;

    public CreateRegion(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Result> ExecuteAsync(CreateRegionRequest request, CancellationToken ct = default)
    {
        try
        {
            var region = new Domain.Entities.Region
            {
                Id = request.Name,
                PosX = request.PosX,
                PosY = request.PosY,
                Width = request.Width,
                Height = request.Height,
                IsHash = request.IsHash,
                IsColor = request.IsColor,
                IsBoard = request.IsBoard,
                Color = request.Color
            };

            using var session = _documentStore.LightweightSession();
            var existingRegion = await session.LoadAsync<Domain.Entities.Region>(region.Id, ct);

            if (existingRegion != null)
                return Result.Conflict($"Region with name {region.Id} already exists.");

            if(existingRegion != null && existingRegion.DeletedDate != null)
            {
                existingRegion.DeletedDate = null;
                session.Store(existingRegion);
                await session.SaveChangesAsync(ct);
                return Result.Success();
            }

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

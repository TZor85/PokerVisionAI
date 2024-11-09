using PokerVisionAI.Domain.Dtos;

namespace PokerVisionAI.Domain.Mappers;

public static class RegionDTOMapper
{
    public static RegionDTO ToDto(this Entities.Region region)
    {
        return new RegionDTO
        {
            Name = region.Id,
            PosX = region.PosX,
            PosY = region.PosY,
            Width = region.Width,
            Height = region.Height,
            IsHash = region.IsHash,
            IsColor = region.IsColor,
            IsBoard = region.IsBoard,
            Color = region.Color
        };
    }

    public static Entities.Region ToTagEntity(this RegionDTO region)
    {
        return new Entities.Region
        {
            Id = region.Name,
            PosX = region.PosX,
            PosY = region.PosY,
            Width = region.Width,
            Height = region.Height,
            IsHash = region.IsHash,
            IsColor = region.IsColor,
            IsBoard = region.IsBoard,
            Color = region.Color
        };
    }
}

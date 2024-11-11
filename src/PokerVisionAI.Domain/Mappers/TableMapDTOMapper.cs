using PokerVisionAI.Domain.Dtos;

namespace PokerVisionAI.Domain.Mappers;

public static class TableMapDTOMapper
{
    public static TableMapDTO ToDto(this Entities.TableMap tableMap)
    {
        var regions = new List<RegionDTO>();

        if (tableMap.Regions != null)
        {
            foreach (var region in tableMap.Regions)
            {
                regions.Add(region.ToDto());
            }
        }

        return new TableMapDTO
        {
            Name = tableMap.Id,
            Regions = regions
        };
    }

    public static Entities.TableMap ToTagEntity(this TableMapDTO tableMap)
    {
        var regions = new List<Entities.Region>();

        if (tableMap.Regions != null)
        {
            foreach (var region in tableMap.Regions)
            {
                regions.Add(region.ToTagEntity());
            }
        }

        return new Entities.TableMap
        {
            Id = tableMap.Name,
            Regions = regions
        };
    }
}

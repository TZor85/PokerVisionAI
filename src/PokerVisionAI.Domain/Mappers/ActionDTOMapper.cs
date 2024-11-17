using PokerVisionAI.Domain.Dtos;

namespace PokerVisionAI.Domain.Mappers;

public static class ActionDTOMapper
{
    public static ActionDTO ToDto(this Entities.Action action)
    {        
        return new ActionDTO
        {
            Name = action.Id,
            Positions = action.Positions?.Select(p => p).ToList()
        };
    }

    public static Entities.Action ToEntity(this ActionDTO action)
    {
        return new Entities.Action
        {
            Id = action.Name,
            Positions = action.Positions?.Select(p => p).ToList()
        };
    }
}

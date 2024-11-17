using PokerVisionAI.Domain.ValueObjects;

namespace PokerVisionAI.Domain.Dtos;

public class ActionDTO
{
    public required string Name { get; set; }
    public List<Position>? Positions { get; set; }
}

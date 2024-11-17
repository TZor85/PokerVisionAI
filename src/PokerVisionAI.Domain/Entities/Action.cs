using PokerVisionAI.Domain.ValueObjects;

namespace PokerVisionAI.Domain.Entities;

public class Action
{
    public required string Id { get; set; }
    public List<Position>? Positions { get; set; }
}

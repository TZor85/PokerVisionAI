using PokerVisionAI.Domain.Enum;

namespace PokerVisionAI.Domain.ValueObjects;

public record BettingAction(string Player, ActionType Action, decimal Amount, DateTime Date);

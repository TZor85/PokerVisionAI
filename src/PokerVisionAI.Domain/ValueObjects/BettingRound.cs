using PokerVisionAI.Domain.Entities;
using PokerVisionAI.Domain.Enum;

namespace PokerVisionAI.Domain.ValueObjects;

public record BettingRound(BettingRoundType Type, List<BettingAction> Actions, List<Card> CommunityCards);
using PokerVisionAI.Domain.Entities;
using PokerVisionAI.Domain.Enum;

namespace PokerVisionAI.Domain.ValueObjects;

public record Player(string Name, decimal? CurrentBet, decimal? CurrentStack,
                        PlayerStatus Status, TablePosition Position, List<Card>? Cards);
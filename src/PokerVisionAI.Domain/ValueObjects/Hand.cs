﻿namespace PokerVisionAI.Domain.ValueObjects;

public record Hand(string Name, bool? Suited, string Action, int Percentage);


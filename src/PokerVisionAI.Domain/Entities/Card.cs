﻿namespace PokerVisionAI.Domain.Entities;

public class Card
{
    public required string Id { get; set; }
    public string? ImageEncrypted { get; set; }
    public string? ImageBase64 { get; set; }
    public string? BinaryValue { get; set; }
    public List<string>? Hall { get; set; }
    public int Force { get; set; }
    public int Suit { get; set; }
}

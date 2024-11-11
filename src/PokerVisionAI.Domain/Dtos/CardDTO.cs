﻿
namespace PokerVisionAI.Domain.Dtos;

public class CardDTO
{
    public required string Name { get; set; }
    public string? ImageBase64 { get; set; }
    public string? ImageEncrypted { get; set; }
    public string? BinaryValue { get; set; }
    public string? Hall { get; set; }
    public int Force { get; set; }
    public int Suit { get; set; }

}
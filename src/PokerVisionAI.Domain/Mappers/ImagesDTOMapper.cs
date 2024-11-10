using PokerVisionAI.Domain.Dtos;

namespace PokerVisionAI.Domain.Mappers;

public static class ImagesDTOMapper
{
    public static ImageDTO ToDto(this Entities.Image images)
    {
        return new ImageDTO
        {
            Name = images.Id,
            BinaryValue = images.BinaryValue,
            ImageBase64 = images.ImageBase64,
            ImageEncrypted = images.ImageEncrypted,
            Force = images.Force,
            Suit = images.Suit
        };
    }

    public static Entities.Image ToTagEntity(this ImageDTO images)
    {
        return new Entities.Image
        {
            Id = images.Name,
            BinaryValue = images.BinaryValue,
            ImageBase64 = images.ImageBase64,
            ImageEncrypted = images.ImageEncrypted,
            Force = images.Force,
            Suit = images.Suit
        };
    }
}

using PokerVisionAI.Features.Images;
using PokerVisionAI.Features.Images.Create;
using PokerVisionAI.Features.Images.Get;
using PokerVisionAI.Features.Images.List;
using PokerVisionAI.Features.Images.Update;
using PokerVisionAI.Features.Regions;
using PokerVisionAI.Features.Regions.Create;
using PokerVisionAI.Features.Regions.Delete;
using PokerVisionAI.Features.Regions.Get;
using PokerVisionAI.Features.Regions.List;
using PokerVisionAI.Features.Regions.Update;

namespace PokerVisionAI.Features;

public static class Services
{
    public static IServiceCollection AddUseCases(this IServiceCollection services) =>
        services
            .AddScoped<CreateRegion>()
            .AddScoped<GetRegion>()
            .AddScoped<ListRegions>()
            .AddScoped<UpdateRegion>()
            .AddScoped<DeleteRegion>()
            .AddScoped<RegionUseCases>()
            
            .AddScoped<CreateImage>()
            .AddScoped<GetImage>()
            .AddScoped<ListImages>()
            .AddScoped<UpdateImage>()
            .AddScoped<ImageUseCases>();

}

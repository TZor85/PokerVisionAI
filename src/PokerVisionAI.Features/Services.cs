using PokerVisionAI.Features.Images;
using PokerVisionAI.Features.Images.Create;
using PokerVisionAI.Features.Images.Delete;
using PokerVisionAI.Features.Images.Get;
using PokerVisionAI.Features.Images.List;
using PokerVisionAI.Features.Regions;
using PokerVisionAI.Features.Regions.Create;
using PokerVisionAI.Features.Regions.Delete;
using PokerVisionAI.Features.Regions.Get;
using PokerVisionAI.Features.Regions.List;
using PokerVisionAI.Features.Regions.Update;
using PokerVisionAI.Features.TableMap;
using PokerVisionAI.Features.TableMap.Create;
using PokerVisionAI.Features.TableMap.Delete;
using PokerVisionAI.Features.TableMap.Get;
using PokerVisionAI.Features.TableMap.List;

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
            .AddScoped<DeleteImage>()
            .AddScoped<ImageUseCases>()
        
            .AddScoped<CreateTableMap>()
            .AddScoped<GetTableMap>()
            .AddScoped<ListTableMaps>()
            .AddScoped<DeleteTableMap>()
            .AddScoped<TableMapUseCases>();

}

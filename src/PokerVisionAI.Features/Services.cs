using PokerVisionAI.Features.Action;
using PokerVisionAI.Features.Action.Create;
using PokerVisionAI.Features.Card;
using PokerVisionAI.Features.Card.Create;
using PokerVisionAI.Features.Card.CreateAll;
using PokerVisionAI.Features.Card.Delete;
using PokerVisionAI.Features.Card.Get;
using PokerVisionAI.Features.Card.List;
using PokerVisionAI.Features.InitialConfig.Action;
using PokerVisionAI.Features.InitialConfig.Card;
using PokerVisionAI.Features.InitialConfig.Region;
using PokerVisionAI.Features.InitialConfig.TableMap;
using PokerVisionAI.Features.Regions;
using PokerVisionAI.Features.Regions.Create;
using PokerVisionAI.Features.Regions.CreateAll;
using PokerVisionAI.Features.Regions.Get;
using PokerVisionAI.Features.Regions.List;
using PokerVisionAI.Features.Regions.Update;
using PokerVisionAI.Features.TableMap;
using PokerVisionAI.Features.TableMap.Create;
using PokerVisionAI.Features.TableMap.CreateAll;
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
            .AddScoped<CreateAllRegions>()
            .AddScoped<RegionUseCases>()
            
            .AddScoped<CreateCard>()
            .AddScoped<GetCard>()
            .AddScoped<ListCards>()
            .AddScoped<DeleteCard>()
            .AddScoped<CreateAllCards>()
            .AddScoped<CardUseCases>()
        
            .AddScoped<CreateTableMap>()
            .AddScoped<GetTableMap>()
            .AddScoped<ListTableMaps>()
            .AddScoped<DeleteTableMap>()
            .AddScoped<CreateAllTableMap>()
            .AddScoped<TableMapUseCases>()

            .AddScoped<CreateAction>()
            .AddScoped<ActionUseCases>()

            .AddScoped<GetAllRegions>()
            .AddScoped<GetAllCards>()
            .AddScoped<GetAllTableMaps>()
            .AddScoped<GetAllActions>();

    

}

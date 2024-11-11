using PokerVisionAI.Features.InitialConfig.TableMap;
using PokerVisionAI.Features.TableMap.Create;
using PokerVisionAI.Features.TableMap.CreateAll;
using PokerVisionAI.Features.TableMap.Delete;
using PokerVisionAI.Features.TableMap.Get;
using PokerVisionAI.Features.TableMap.List;

namespace PokerVisionAI.Features.TableMap;

public record TableMapUseCases(CreateTableMap CreateTableMap, GetTableMap GetTableMap, ListTableMaps ListTableMaps, DeleteTableMap DeleteTableMap, CreateAllTableMap CreateAllTableMaps);


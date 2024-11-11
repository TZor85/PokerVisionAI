﻿using PokerVisionAI.Domain.Mappers;
using PokerVisionAI.Features.Card.CreateAll;
using PokerVisionAI.Features.Regions.CreateAll;
using PokerVisionAI.Features.TableMap.CreateAll;

namespace PokerVisionAI.App.Components.Pages
{
    public partial class PageInitConfig
    {
        private bool isInitializingRegions;
        private bool isInitializingCards;
        private bool isInitializingTableMap;
        private string? regionsMessage;
        private string? cardsMessage;
        private string? tableMapMessage;
        private bool regionsSuccess;
        private bool cardsSuccess;
        private bool tableMapSuccess;

        private async Task InitializeRegions()
        {
            try
            {
                isInitializingRegions = true;
                regionsMessage = null;

                var regions = new List<Domain.Entities.Region>();
                var regionsDto = await _allRegions.Executesync();

                foreach (var item in regionsDto)
                {
                    regions.Add(item.ToEntity());
                }

                await _regionUseCases.CreateAllRegions.ExecuteAsync(new CreateAllRegionsRequest(regions));

                regionsMessage = "Regiones inicializadas correctamente";
                regionsSuccess = true;
            }
            catch (Exception ex)
            {
                regionsMessage = $"Error al inicializar regiones: {ex.Message}";
                regionsSuccess = false;
            }
            finally
            {
                isInitializingRegions = false;
            }
        }

        private async Task InitializeCards()
        {
            try
            {
                isInitializingCards = true;
                cardsMessage = null;

                var cards = new List<Domain.Entities.Card>();
                var cardsDto = await _allCards.Executesync();

                foreach (var item in cardsDto)
                {
                    cards.Add(item.ToEntity());
                }

                await _cardUseCases.CreateAllCards.ExecuteAsync(new CreateAllCardsRequest(cards));

                cardsMessage = "Cartas inicializadas correctamente";
                cardsSuccess = true;
            }
            catch (Exception ex)
            {
                cardsMessage = $"Error al inicializar cartas: {ex.Message}";
                cardsSuccess = false;
            }
            finally
            {
                isInitializingCards = false;
            }
        }

        private async Task InitializeTableMap()
        {
            try
            {
                isInitializingTableMap = true;
                tableMapMessage = null;

                var tableMap = new List<Domain.Entities.TableMap>();
                var tableMapDto = await _allTableMaps.Executesync();

                foreach (var item in tableMapDto)
                {
                    tableMap.Add(item.ToEntity());
                }

                await _tableMapUseCases.CreateAllTableMaps.ExecuteAsync(new CreateAllTableMapRequest(tableMap));

                tableMapMessage = "TableMap inicializados correctamente";
                tableMapSuccess = true;

            }
            catch (Exception ex)
            {
                tableMapMessage = $"Error al inicializar TableMap: {ex.Message}";
                tableMapSuccess = false;
            }
            finally
            {
                isInitializingTableMap = false;
            }
        }
    }
}
﻿using PokerVisionAI.Features.Regions.Create;
using PokerVisionAI.Features.Regions.Delete;
using PokerVisionAI.Features.Regions.Get;
using PokerVisionAI.Features.Regions.List;
using PokerVisionAI.Features.Regions.Update;

namespace PokerVisionAI.Features.Regions;

public record RegionUseCases(CreateRegion CreateRegion, GetRegion GetRegion, ListRegions ListRegions, UpdateRegion UpdateRegion, DeleteRegion DeleteRegion);

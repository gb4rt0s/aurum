﻿using Aurum.Data.Entities;
using Aurum.Models.LayoutDTOs;

namespace Aurum.Repositories.LayoutRepository
{
    public interface ILayoutRepo
    {
        Task<int> CreateBasic(BasicLayout layout);
        Task<int> UpdateBasic(BasicLayout layout);
        Task<int> CreateScientic(ScienticLayout layout);
        Task<int> UpdateScientic(ScienticLayout layout);
        Task<int> CreateDetailed(DetailedLayout layout);
        Task<int> UpdateDetailed(DetailedLayout layout);
        Task<BasicLayout>? GetBasic(string userId);
        Task<ScienticLayout>? GetScientic(string userId);
        Task<DetailedLayout>? GetDetailed(string userId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseAPI.Models;

namespace HouseAPI.Repositories
{
    public interface IHouseRepo
    {
        Task<HouseList> PostHouse(HouseList item);
        IEnumerable<HouseList> GetFreeHouses();
    }
}

using HouseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseAPI.Repositories
{
    public class HouseRepo:IHouseRepo
    {

        private readonly CommunityGateDatabaseContext _context;

        public HouseRepo()
        {

        }
        public HouseRepo(CommunityGateDatabaseContext context)
        {
            _context = context;
        }


        public async Task<HouseList> PostHouse(HouseList item)
        {
            HouseList house = null;
            if (item == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                house = new HouseList()
                {
                    HouseId = item.HouseId,
                    IsFree = item.IsFree
                };
                await _context.HouseList.AddAsync(house);
                await _context.SaveChangesAsync();
            }
            return house;
        }
    }
}

using NUnit.Framework;
using HouseAPI;
using HouseAPI.Controllers;
using HouseAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Microsoft.EntityFrameworkCore;
using HouseAPI.Repositories;

namespace HouseNunitTest
{
    public class Tests
    {
        List<HouseList> houses = new List<HouseList>();
        IQueryable<HouseList> housedata;
        Mock<DbSet<HouseList>> mockSet;
        Mock<CommunityGateDatabaseContext> communityGateContextMock;
        [SetUp]
        public void Setup()
        {
            houses = new List<HouseList>()
            {
                new HouseList{HouseId = 1, IsFree = "Free"},
                new HouseList{HouseId = 2, IsFree = "Occupied"},
                new HouseList{HouseId = 3, IsFree = "Free"}
            };
            housedata = houses.AsQueryable();
            mockSet = new Mock<DbSet<HouseList>>();
            mockSet.As<IQueryable<HouseList>>().Setup(m => m.Provider).Returns(housedata.Provider);
            mockSet.As<IQueryable<HouseList>>().Setup(m => m.Expression).Returns(housedata.Expression);
            mockSet.As<IQueryable<HouseList>>().Setup(m => m.ElementType).Returns(housedata.ElementType);
            mockSet.As<IQueryable<HouseList>>().Setup(m => m.GetEnumerator()).Returns(housedata.GetEnumerator());
            var p = new DbContextOptions<CommunityGateDatabaseContext>();
            communityGateContextMock = new Mock<CommunityGateDatabaseContext>(p);
            communityGateContextMock.Setup(x => x.HouseList).Returns(mockSet.Object);
        }

        [Test]
        public void PostHouseTest()
        {
            var houseRepo = new HouseRepo(communityGateContextMock.Object);
            var houseObj = houseRepo.PostHouse(new HouseList { HouseId = 4, IsFree = "Free" });
            Assert.IsNotNull(houseObj);
        }

        [Test]
        public void GetFreeHousesTest()
        {
            var houseRepo = new HouseRepo(communityGateContextMock.Object);
            var houseList = houseRepo.GetFreeHouses();
            Assert.AreEqual(2, houseList.Count());
        }
        [Test]
        public void  updateHouseStatusTest()
        {
            var houseRepo = new HouseRepo(communityGateContextMock.Object);
            var houseObj = houseRepo.UpdateIsFreeHouse(1);
            Assert.IsNotNull(houseObj);
        }
    }
}
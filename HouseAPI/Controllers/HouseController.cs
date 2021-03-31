using HouseAPI.Models;
using HouseAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(HouseController));
        private readonly IHouseRepo _context;
        public HouseController(IHouseRepo context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostHouse(HouseList item)
        {
            _log4net.Info("Post House Was Called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var addHouse = await _context.PostHouse(item);
                return Ok(addHouse);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }

}

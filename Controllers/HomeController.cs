using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaderboardApi.Models;
using StackExchange.Redis;
namespace leaderboard.Controllers
{

    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        [HttpPost]
        public JsonResult Index([FromBody] Player player)
        {
            this.HttpContext.Response.StatusCode = 201;
            return Json(player.save());
        }


        // 
        // GET: /HelloWorld/Welcome/ 

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}

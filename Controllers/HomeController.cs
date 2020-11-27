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
            if (player.create())
            {
                this.HttpContext.Response.StatusCode = 201;
                return Json(player);
            }
            this.HttpContext.Response.StatusCode = 400;
            string response = "{status:bad request}";
            return Json(response);
        }



        [HttpGet("{nickname}")]
        public string Get(string nickname)
        {

            return "";
        }

        [HttpPut("{nickname}", Name="update")]
        public JsonResult Put([FromBody] Player player, string nickname)
        {
            Player updated_player = player.update(nickname);
            if (updated_player!=null)
            {
                return Json(updated_player);
            }
            this.HttpContext.Response.StatusCode = 400;
            string response = "{status:bad request}";
            return Json(response);
        }
    }
}

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
            return Json("");
        }



        [HttpGet("{nickname}")]
        public JsonResult Get(string nickname)
        {
            Player player = new Player(nickname, 0);
            player = player.getPlayer(nickname);
            if (player!=null)
            {
                return Json(player);
            }
            this.HttpContext.Response.StatusCode = 404;
            return Json("");
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
            return Json("");
        }

        [HttpDelete("{nickname}", Name ="delete")]
        public JsonResult Delete(string nickname)
        {
            Player player = new Player(nickname, 0);
            if (player.delete(nickname))
            {
                this.HttpContext.Response.StatusCode = 204;
                return Json("");
            }
            this.HttpContext.Response.StatusCode = 404;
            return Json("");
        }
    }
}

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

        [HttpGet("{nickname}", Name ="get")]
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

        [HttpGet("leaderboard", Name ="get leaderboard")]
        public JsonResult get_leaderboard()
        {
            ConnectionMultiplexer muxer = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase redis =  muxer.GetDatabase();
            string redis_response = redis.StringGet("leaderboard");
            redis_response = redis_response.Trim(new Char[] { '[', ']' });
            string[] player_list = redis_response.Split("|");
            (int, string)[]players =  new (int, string)[player_list.Length-1];
            
            for(int i=0; i < player_list.Length; i++)
            {
                string score = redis.StringGet(player_list[i]);
                if (score != null)
                {
                    players[i] = (Int32.Parse(score), player_list[i]);  
                }
            }
            Array.Sort(players);
            Array.Reverse(players);
            List<Player> leaderboard = new List<Player>();
            for (int i = 0; i < players.Length; i++)
            {
                leaderboard.Add(new Player(players[i].Item2, players[i].Item1));
            }
                return Json(leaderboard);
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

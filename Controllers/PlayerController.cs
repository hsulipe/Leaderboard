using Leaderboard.Models;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using Leaderboard.Services.Leaderboards;

namespace leaderboard.Controllers
{

    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        private readonly ILeaderboardService _leaderboardService;

        public PlayerController(ILeaderboardService leaderboardService)
        {
            _leaderboardService = leaderboardService;
        }

        [HttpPost]
        // Use ActionResult is better than just JsonResult
        public ActionResult<Player> Index([FromBody] Player player)
        {
            try
            {
                _leaderboardService.AddAsync(player).GetAwaiter().GetResult();
                return Ok(player);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("{nickname}", Name ="get")]
        public ActionResult<Player> Get(string nickname)
        {
            try
            {
                return Ok(_leaderboardService.GetByAsync(nickname).GetAwaiter().GetResult());
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("leaderboard", Name ="get leaderboard")]
        public ActionResult<Player[]> get_leaderboard()
        {
            try
            {
                return Ok(_leaderboardService.GetAsync().GetAwaiter().GetResult());
            }
            catch (Exception)
            {
                return BadRequest();
            }

            // Duplication of code

            //ConnectionMultiplexer muxer = ConnectionMultiplexer.Connect("localhost:6379");
            //IDatabase redis =  muxer.GetDatabase();
            //string redis_response = redis.StringGet("leaderboard");
            //redis_response = redis_response.Trim(new Char[] { '[', ']' });
            //string[] player_list = redis_response.Split("|");
            //(int, string)[]players =  new (int, string)[player_list.Length-1];

            //for(int i=0; i < player_list.Length; i++)
            //{
            //    string score = redis.StringGet(player_list[i]);
            //    if (score != null)
            //    {
            //        players[i] = (Int32.Parse(score), player_list[i]);  
            //    }
            //}
            //Array.Sort(players);
            //Array.Reverse(players);
            //List<Player> leaderboard = new List<Player>();
            //for (int i = 0; i < players.Length; i++)
            //{
            //    leaderboard.Add(new Player(players[i].Item2, players[i].Item1));
            //}
            //    return Json(leaderboard);
        }

        // Not Necessary

        //[HttpPut("{nickname}", Name="update")]
        //public JsonResult Put([FromBody] Player player, string nickname)
        //{
        //    Player updated_player = player.update(nickname);
        //    if (updated_player!=null)
        //    {
        //        return Json(updated_player);
        //    }
        //    this.HttpContext.Response.StatusCode = 400;
        //    return Json("");
        //}

        [HttpDelete("{nickname}", Name ="delete")]
        public ActionResult<bool> Delete(string nickname)
        {
            try
            {
                return Ok(_leaderboardService.RemoveByAsync(nickname).GetAwaiter().GetResult());
            }
            catch (Exception)
            {
                return BadRequest();
            }
            //Player player = new Player(nickname, 0);
            //if (player.delete(nickname))
            //{
            //    this.HttpContext.Response.StatusCode = 200;
            //    return Json("");
            //}
            //this.HttpContext.Response.StatusCode = 404;
            //return Json("");
        }
    }
}

using System;
using System.Collections.Generic;

/// <summary>
/// Summary description for Class1
/// </summary>
using StackExchange.Redis;

namespace LeaderboardApi.Models
{
    public class Player
    {
        
        public string Nickname { get; set; }
        public int Score { get; set; }
        private IDatabase Redis { get; set; }
        public Player(string nickname, int score)
        {
            this.Nickname = nickname;
            this.Score = score;
            this.Redis = redis();
        }
        private static IDatabase redis()
        {
            ConnectionMultiplexer muxer = ConnectionMultiplexer.Connect("localhost:6379");
            return muxer.GetDatabase();
        }

        public Player save()
        {
            this.Redis.StringSet(this.Nickname, this.Score);
            string redis_response = this.Redis.StringGet("leaderboard");
            if (redis_response==null) {
                this.Redis.StringSet("leaderboard", this.Nickname+"|");
            }
            else
            {
                list_player(true);
            }
            return this;
        }

      

        // use operation true para adição e false para deletar
        private string list_player(bool operation)
        {
            string redis_response = this.Redis.StringGet("leaderboard");
            redis_response = redis_response.Trim(new Char[] { '[', ']'});
            if (operation)
            {
                redis_response = redis_response + this.Nickname;
            }
         
            string[] player_list = redis_response.Split("|");
            /*(string, int)[] players = null;

            for(int i=0; i < player_list.Length; i++)
            {
                string score = this.Redis.StringGet(player_list[i]);
                players[i] = (player_list[i], Int32.Parse(score));
            }
            Array.Sort(players);
            */
            redis_response = "[";
            foreach(string player in player_list)
            {
                redis_response = $"{redis_response}{player}|";
            }
            this.Redis.StringSet("leaderboard",redis_response + "]");
            return "";
        }
    }
}

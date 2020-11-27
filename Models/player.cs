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
        // Atributos 
        public string Nickname { get; set; }
        public int Score { get; set; }
        private IDatabase Redis { get; set; }

        // construtor 
        public Player(string nickname, int score)
        {
            this.Nickname = nickname;
            this.Score = score;
            this.Redis = redis();
        }

  
        // Metodos publicos
        public bool create()
        {
            string redis_response = this.Redis.StringGet(this.Nickname);
            if (redis_response == null)
            {
                this.Redis.StringSet(this.Nickname, this.Score);
                redis_response = this.Redis.StringGet("leaderboard");
                if (redis_response==null) {
                    this.Redis.StringSet("leaderboard", this.Nickname+"|");
                }
                else
                {
                    add_list(redis_response);
                }
                return true;

            }
            return false;
        }

        public Player getPlayer(string nickname)
        {
            string redis_response = this.Redis.StringGet(nickname);
            if (redis_response != null)
            {
                this.Nickname = nickname;
                this.Score = Int32.Parse(redis_response);
                return this;
            }
            return null;
        }

        public Player update(string nickname)
        {
            string redis_response = this.Redis.StringGet(nickname);
            if (redis_response != null)
            {
                if (this.Nickname != nickname)
                {
                    this.Redis.KeyDelete(nickname);
                }
                redis_response = this.Redis.StringGet(this.Nickname);
                if (redis_response != null)
                {
                    return null;
                }
                this.Redis.StringSet(this.Nickname, this.Score);
                
                update_list(nickname);
                return this;

            }
            return null;
        }

        public bool delete(string nickname)
        {
            string redis_response = this.Redis.StringGet(nickname);
            if (redis_response!=null)
            {
                this.Redis.KeyDelete(nickname);
                delete_list(nickname);
                return true;
            }
            return false;
        }
      
        // Metodos privados
        private void add_list(string leaderbord)
        {
            leaderbord = leaderbord.Trim(new Char[] { '[', ']'});
            leaderbord = leaderbord + this.Nickname;
            
         
            string[] player_list = leaderbord.Split("|");
            /*(string, int)[] players = null;

            for(int i=0; i < player_list.Length; i++)
            {
                string score = this.Redis.StringGet(player_list[i]);
                players[i] = (player_list[i], Int32.Parse(score));
            }
            Array.Sort(players);
            */
            leaderbord = "[";
            foreach(string player in player_list)
            {
                leaderbord = $"{leaderbord}{player}|";
            }
            this.Redis.StringSet("leaderboard", leaderbord + "]");   
        }

        private void update_list(string nickname)
        {
            string redis_response = this.Redis.StringGet("leaderboard");
            redis_response = redis_response.Trim(new Char[] { '[', ']' });


            string[] player_list = redis_response.Split("|");

            redis_response = "[";
            foreach (string player in player_list)
            {
                if(player != "")
                {
                    if (nickname == player)
                    {
                        redis_response = $"{redis_response}{this.Nickname}|";
                    }
                    else
                    {
                        redis_response = $"{redis_response}{player}|";
                    }
                }
                
            }
            this.Redis.StringSet("leaderboard", redis_response + "]");
        }

        private void delete_list(string nickname)
        {
            string redis_response = this.Redis.StringGet("leaderboard");
            redis_response = redis_response.Trim(new Char[] { '[', ']' });


            string[] player_list = redis_response.Split("|");

            redis_response = "[";
            foreach (string player in player_list)
            {
                if (player != "")
                {
                    if (nickname != player)
                    {
                        redis_response = $"{redis_response}{player}|";
                        
                    } 
                }

            }
            this.Redis.StringSet("leaderboard", redis_response + "]");
        }
        // Acesso ao banco de dados
        private static IDatabase redis()
        {
            ConnectionMultiplexer muxer = ConnectionMultiplexer.Connect("localhost:6379");
            return muxer.GetDatabase();
        }
    }
}

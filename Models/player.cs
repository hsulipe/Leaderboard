using System;

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

        public Player(string nickname, int score)
        {
            this.Nickname = nickname;
            this.Score = score;
        }

        
        public Player save()
        {
            ConnectionMultiplexer muxer = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase conn = muxer.GetDatabase();
            conn.StringSet(this.Nickname, this.Score);
            return this;
        }

       
    }
}

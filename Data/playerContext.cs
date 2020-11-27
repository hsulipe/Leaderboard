using Microsoft.EntityFrameworkCore;
using leaderboard.Models;

namespace LeaderboardApi.Data
{
    public class PlayerContext : DbContext
    {
        public PlayerContext(DbContextOptions<PlayerContext> options)
            : base(options)
        {
        }

        public DbSet<Players> Players { get; set; }
    }
}
using Leaderboard.Models;
using System.Threading.Tasks;

namespace Leaderboard.Services.Leaderboards
{
    public interface ILeaderboardService
    {
        Task AddAsync(Player item);
        Task<Player[]> GetAsync();
        Task<Player> GetByAsync(string nickname);
        Task<bool> RemoveByAsync(string nickname);
    }
}

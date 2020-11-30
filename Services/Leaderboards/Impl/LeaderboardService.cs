using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Leaderboard.Infrastructure.Redis;
using Leaderboard.Models;
using StackExchange.Redis;

namespace Leaderboard.Services.Leaderboards.Impl
{
    public class LeaderboardService : ILeaderboardService
    {
        private const string key = "leaderboard";
        private readonly IDatabase _database;
        public LeaderboardService(IRedisConnection connection)
        {
            _database = connection.GetDatabase();
        }

        public async Task AddAsync(Player item)
        {
            await _database.SortedSetAddAsync(key, item.Nickname, item.Score);
        }

        public async Task<Player[]> GetAsync()
        {
            var result = await _database.SortedSetRangeByRankWithScoresAsync(key, 0, -1, Order.Descending);
            return result.Select(x => new Player(x.Element, x.Score)).ToArray();
        }

        public async Task<Player> GetByAsync(string nickname)
        {
            var result = await _database.SortedSetScoreAsync(key, nickname);
            if (result != null) return new Player(nickname, (double) result);
            throw new KeyNotFoundException();
        }

        public async Task<bool> RemoveByAsync(string nickname)
            => await _database.SortedSetRemoveAsync(key, nickname);
    }
}

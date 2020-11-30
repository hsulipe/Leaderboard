using StackExchange.Redis;

namespace Leaderboard.Infrastructure.Redis
{
    public interface IRedisConnection
    {
        IDatabase GetDatabase();
    }
}

using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Leaderboard.Infrastructure.Redis.Impl
{
    public class RedisConnection : IRedisConnection
    {
        private readonly ConnectionMultiplexer _connection;
        public RedisConnection(IConfiguration configuration)
        {
            _connection = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
        }

        public IDatabase GetDatabase()
            => _connection.GetDatabase();
    }
}

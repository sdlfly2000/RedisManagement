using StackExchange.Redis;

namespace Core.RedisClient
{
    public static class RedisConnector
    {
        private static ConnectionMultiplexer _redisClient;
        private static object _lock = new();
        public static ConnectionMultiplexer GetRedisClient(string connectionString)
        {
            if (_redisClient is null)
            {
                lock (_lock)
                {
                    if (_redisClient is null)
                    {
                        _redisClient = ConnectionMultiplexer.Connect(connectionString);
                    }
                }
            }

            return _redisClient;
        }
    }
}

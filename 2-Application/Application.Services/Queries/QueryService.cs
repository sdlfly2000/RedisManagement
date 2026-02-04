using Common.Core.DependencyInjection;
using Core.RedisClient;
using StackExchange.Redis;

namespace Application.Services.Queries
{
    [ServiceLocate(default)]
    public class QueryService
    {
        public async Task<int> Query(string pattern, string host, string accessKey, CancellationToken token)
        {
            var queryKeyCountScript = $"return #redis.pcall('keys', '{pattern}')";
            var connectionString = $"{host},password={accessKey},ssl=true";
            var redisScript = LuaScript.Prepare(queryKeyCountScript);
            using var redis = RedisConnector.GetRedisClient(connectionString);
            var db = redis.GetDatabase();
            var result = await db.ScriptEvaluateAsync(redisScript).ConfigureAwait(false);

            return (int)result;
        }
    }
}

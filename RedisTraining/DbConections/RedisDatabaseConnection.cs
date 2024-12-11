using StackExchange.Redis;

namespace RedisTraining.DbConections;

public class RedisDatabaseConnection
{
    private readonly ConnectionMultiplexer _redis;
    private readonly IDatabase _db;

    public RedisDatabaseConnection(string connectionString = "localhost:6380")
    {
        _redis = ConnectionMultiplexer.Connect(connectionString);
        _db = _redis.GetDatabase();
    }
public IDatabase GetConection()
    {
        return _db;
    }
}


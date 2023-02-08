using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using POC.RedisAPI.Models;
using StackExchange.Redis;

namespace POC.RedisAPI.Data
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly IConnectionMultiplexer _redis;

        public PlatformRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
            {
                throw new ArgumentOutOfRangeException(nameof(platform));
            }

            var db = _redis.GetDatabase();

            var serialPlat = JsonSerializer.Serialize(platform);

            //db.StringSet(plat.Id, serialPlat);
            db.HashSet($"hashplatform", new HashEntry[] {new HashEntry(platform.Id, serialPlat)});
        }

        public IEnumerable<Platform> GetAllPlatform()
        {
            var db = _redis.GetDatabase();

            var completeSet = db.HashGetAll("hashplatform");

            if (completeSet.Length > 0)
            {
                var obj = Array.ConvertAll(completeSet, val =>
                    JsonSerializer.Deserialize<Platform>(val.Value)).ToList();
                return obj;
            }

            return null;
        }

        public Platform GetPlatformById(string id)
        {
            var db = _redis.GetDatabase();

            //var plat = db.StringGet(id);

            var plat = db.HashGet("hashplatform", id);

            if (!string.IsNullOrEmpty(plat))
            {
                return JsonSerializer.Deserialize<Platform>(plat);
            }
            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POC.RedisAPI.Models;

namespace POC.RedisAPI.Data
{
    public interface IPlatformRepository
    {
        void CreatePlatform(Platform platform);
        Platform GetPlatformById(string id);
        IEnumerable<Platform> GetAllPlatform();
    }
}
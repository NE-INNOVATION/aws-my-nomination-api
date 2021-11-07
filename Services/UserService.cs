using MongoDB.Driver;
using my_nomination_api.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace my_nomination_api.Services
{
    public class UserService
    {
        private readonly IMongoCollection<UserRegion> _userRegion;
        private readonly IMongoCollection<GroupConfig> _groupConfig;

        public UserService(IMyNominationDatabaseSettings settings)
        {
            var client = new MongoClient(Environment.GetEnvironmentVariable("ConnectionString") ?? settings.ConnectionString);
            var database = client.GetDatabase(Environment.GetEnvironmentVariable("DatabaseName") ?? settings.DatabaseName);
            _userRegion = database.GetCollection<UserRegion>(System.Environment.GetEnvironmentVariable("UserRegionCollectionName") ?? settings.UserRegionCollectionName);
            _groupConfig = database.GetCollection<GroupConfig>(System.Environment.GetEnvironmentVariable("GroupConfigCollectionName") ?? settings.GroupConfigCollectionName);
        }

        public List<UserRegion> GetAllUsers() =>
         _userRegion.Find(userRegion => true).ToList();

        public UserRegion GetUserByEnterpriseId(string enterpriseId) =>
           _userRegion.Find<UserRegion>(userRegion => userRegion.EnterpriseId == enterpriseId).FirstOrDefault();

        public List<UserRegion> GetUsersByRegion(string regionId) =>
            _userRegion.Find<UserRegion>(userRegion => userRegion.RegionId.Contains(regionId)).ToList();

        public UserRegion CreateUser(UserRegion userRegion)
        {
            _userRegion.InsertOne(userRegion);
            return userRegion;
        }

        private List<GroupConfig> GetAllGroup() =>
       _groupConfig.Find(groups => true).ToList();

        public List<Region> GetUserRegionsByGroups(List<string> userGroups)
        {
            var opRegion = new List<Region>();
            var groups = GetAllGroup();
            foreach (var userRegion in userGroups)
            {
                var region = groups.Find(x => x.GroupId.ToString() == userRegion && x.IsRegion == true);
                if (region == null) continue;
                var regionName = region.GroupName.Split("_");
                if (regionName.Length < 4) continue;
                opRegion.Add(new Region { RegionId = region.GroupId.ToString(), RegionName = regionName[3] });
            }

            return opRegion;
        }

        public UserRegion UpdateUser(UserRegion userRegion)
        {
            _userRegion.ReplaceOneAsync(b => b.EnterpriseId == userRegion.EnterpriseId, userRegion);
            return userRegion;
        }
    }
}

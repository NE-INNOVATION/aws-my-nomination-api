using MongoDB.Driver;
using my_nomination_api.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace my_nomination_api.Services
{
    public class EligibilityService
    {
        private readonly IMongoCollection<GroupConfig> _groupConfig;
        private readonly UserService _userService;
        public EligibilityService(IMyNominationDatabaseSettings settings, UserService userService)
        {
            var client = new MongoClient(Environment.GetEnvironmentVariable("ConnectionString") ?? settings.ConnectionString);
            var database = client.GetDatabase(Environment.GetEnvironmentVariable("DatabaseName") ?? settings.DatabaseName);
            _groupConfig = database.GetCollection<GroupConfig>(System.Environment.GetEnvironmentVariable("GroupConfigCollectionName") ?? settings.GroupConfigCollectionName);
            _userService = userService;
        }

        private List<GroupConfig> GetAllGroup() =>
        _groupConfig.Find(groups => true).ToList();

        public EligibiltyResponse GetUserEligibilty(List<string> userGroups)
        {
            var eligibiltyResponse = new EligibiltyResponse();
            eligibiltyResponse.Region = new List<Region>();

            var groups = GetAllGroup();

            foreach (var userRegion in userGroups)
            {
                var userGroup = groups.FirstOrDefault(x => x.GroupId.ToString() == userRegion);
                if (userGroup == null) continue;

                Region region = new Region();
                string groupId = userGroup.GroupId.ToString();

                region.RegionId = groupId;
                region.RegionName = userGroup.GroupName;

                if (groupId == GroupCode.MN_NEIES_Administrator) eligibiltyResponse.IsAdmin = true;
                if (groupId == GroupCode.MN_NEIES_ProgramManager) eligibiltyResponse.IsProgramManager = true;
                if (groupId == GroupCode.MN_NEIES_RegionGroup_EAST) eligibiltyResponse.IsRegionManagerEast = true;
                if (groupId == GroupCode.MN_NEIES_RegionGroup_WEST) eligibiltyResponse.IsRegionManagerWest = true; 
                if (groupId == GroupCode.MN_NEIES_RegionGroup_NORTH) eligibiltyResponse.IsRegionManagerNorth = true; 
                if (groupId == GroupCode.MN_NEIES_RegionGroup_SOUTH) eligibiltyResponse.IsRegionManagerSouth = true;
                if (groupId == GroupCode.MN_NEIES_RegionManager) eligibiltyResponse.IsRegionManager = true;
                                
            }

            eligibiltyResponse.Region = GetUserRegions(userGroups);

            return eligibiltyResponse;

        }

        private List<Region> GetUserRegions(List<string> userGroups)
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
    }
}

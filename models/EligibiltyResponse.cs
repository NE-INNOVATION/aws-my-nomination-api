using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_nomination_api.models
{
    public class EligibiltyResponse
    {
        public bool IsAdmin { get; set; }
        public bool IsRegionManager { get; set; }
        public bool IsProgramManager { get; set; }
        public bool IsRegionManagerEast { get; set; }
        public bool IsRegionManagerWest { get; set; }
        public bool IsRegionManagerNorth { get; set; }
        public bool IsRegionManagerSouth { get; set; }
        public List<Region> Region { get; set; }
    }

    public class Region
    {
        public string RegionId { get; set; }
        public string RegionName { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_nomination_api.models
{
    public class MyNominationDatabaseSettings : IMyNominationDatabaseSettings
    {
        public string ProgramCollectionName { get; set; }
        public string UsersCollectionName { get; set; }
        public string NominationsCollectionName { get; set; }

        public string ProgramCategoryCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMyNominationDatabaseSettings
    {
        string ProgramCollectionName { get; set; }

        string UsersCollectionName { get; set; }

        string NominationsCollectionName { get; set; }

        string ProgramCategoryCollectionName { get; set; }

        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

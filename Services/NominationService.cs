using MongoDB.Driver;
using my_nomination_api.models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace my_nomination_api.Services
{
    public class NominationService
    {
        private readonly IMongoCollection<Nominations> _nominations;
        private readonly IMongoCollection<NominationProgram> _nominationProgram;
        private readonly IMongoCollection<ProgramCategory> _categories;
        private readonly IMongoCollection<User> _users;

        public NominationService(IMyNominationDatabaseSettings settings)
        {
            var client = new MongoClient(Environment.GetEnvironmentVariable("ConnectionString") ?? settings.ConnectionString);
            var database = client.GetDatabase(Environment.GetEnvironmentVariable("DatabaseName") ?? settings.DatabaseName);

            _nominationProgram = database.GetCollection<NominationProgram>(System.Environment.GetEnvironmentVariable("ProgramCollectionName") ?? settings.ProgramCollectionName);
            _nominations = database.GetCollection<Nominations>(System.Environment.GetEnvironmentVariable("NominationsCollectionName") ?? settings.NominationsCollectionName);
            _users = database.GetCollection<User>(System.Environment.GetEnvironmentVariable("UsersCollectionName") ?? settings.UsersCollectionName);
            _categories = database.GetCollection<ProgramCategory>(System.Environment.GetEnvironmentVariable("ProgramCategoryCollectionName") ?? settings.ProgramCategoryCollectionName);
        }

        public List<Nominations> GetAllNominations() =>
          _nominations.Find(book => true).ToList();

        public List<Nominations> GetProgramNominations(string programId) =>
            _nominations.Find<Nominations>(Nominations => Nominations.ProgramId == programId).ToList();

        public Nominations GetNominationDetails(string programId, string EnterpriseId) =>
          _nominations.Find<Nominations>(Nominations => Nominations.ProgramId == programId && Nominations.EnterpriseId == EnterpriseId).FirstOrDefault();

        public Nominations CreateNominations(Nominations nominations)
        {
            _nominations.InsertOne(nominations);
            return nominations;
        }

        public Nominations UpdateNominations(Nominations nominations)
        {
            _nominations.ReplaceOneAsync(b => b.ProgramId == nominations.ProgramId && b.EnterpriseId == nominations.EnterpriseId, nominations);
            return nominations;
        }

        public bool MoveNominations(Nominations nominations)
        {
           return _nominations.ReplaceOneAsync(b => b.EnterpriseId == nominations.EnterpriseId, nominations).IsCompleted;
        }

        public Nominations DeleteNominations(Nominations nominations)
        {
            _nominations.DeleteOneAsync(b => b.ProgramId == nominations.ProgramId && b.EnterpriseId == nominations.EnterpriseId);
            return nominations;
        }

        public NominationProgram GetProgramById(string programId)
        {
           return _nominationProgram.Find<NominationProgram>(Nominations => Nominations.ProgramId == programId).FirstOrDefault();
        }
           

        public NominationProgram CreateNominationProgram(NominationProgram nominationProgram)
        {
            nominationProgram.ProgramId = "MN" + GenerateRandomNo();
            _nominationProgram.InsertOne(nominationProgram);
            return nominationProgram;
        }

        public NominationProgram UpdateNominationProgram(NominationProgram nominationProgram)
        {
             _nominationProgram.ReplaceOneAsync(b => b.ProgramId == nominationProgram.ProgramId, nominationProgram);
            return nominationProgram;
        }

        public List<NominationProgram> GetAllProgram() =>
        _nominationProgram.Find(nominationProgram => true).ToList();

        public List<User> GetAllUsers() =>
       _users.Find(users => true).ToList();

        public List<ProgramCategory> GetAllProgramCategories(User user)
        {
            var categories = _categories.Find(category => true).ToList();

            if (user.CategoryId.Contains("superadmin", StringComparer.OrdinalIgnoreCase))
            {
                return categories;
            }

            var categoryOutput = new List<ProgramCategory>();

            foreach (var category in categories)
            {
                if (user.CategoryId.Contains(category.CategoryId))
                {
                    categoryOutput.Add(category);
                }
            }

            return categoryOutput;

        }
      

        public List<NominationProgram> GetProgramsForCategories(string categoryId)
        {
            var activePrograms = GetAllActiveProgram();
            return activePrograms.Where(activePrograms => activePrograms.categoryId == categoryId).ToList();
        }
          

        public List<Nominations> GetAllNomintion() =>
        _nominations.Find(nominationProgram => true).ToList();

        public List<NominationProgram> GetAllActiveProgram()
        {
            var cultureInfo = new CultureInfo("en-US");
            var allProgram = GetAllProgram();
            var activeProgram = new List<NominationProgram>();

            foreach (var program in allProgram)
            {

                int year = Convert.ToInt32(program.StartDate.Substring(0, 4));
                int month = Convert.ToInt32(program.StartDate.Substring(5, 2));
                int day = Convert.ToInt32(program.StartDate.Substring(8, 2));

                var programStartDate = new DateTime(year, month, day);

                if (programStartDate >= DateTime.Today.Date && program.IsPublished && program.IsClosed == false && program.Status == 1)
                {
                    activeProgram.Add(program);
                }
            }

            return activeProgram;
        }

        public List<NominationProgram> GetCompletedPrograms()
        {
            var cultureInfo = new CultureInfo("en-US");
            var allProgram = GetAllProgram();
            var completedPrograms = new List<NominationProgram>();

            foreach (var program in allProgram)
            {

                int year = Convert.ToInt32(program.EndDate.Substring(0, 4));
                int month = Convert.ToInt32(program.EndDate.Substring(5, 2));
                int day = Convert.ToInt32(program.EndDate.Substring(8, 2));

                var programEndDate = new DateTime(year, month, day);

                if (programEndDate < DateTime.Today.Date 
                    && program.IsPublished)
                {
                    completedPrograms.Add(program);
                }
            }

            return completedPrograms;
        }


        public List<NominationProgram> GetPrograms(User userInput)
        {
            if (userInput.CategoryId.Contains("superadmin", StringComparer.OrdinalIgnoreCase))
            {
                return GetAllProgram();
            }

            var programsForCategory = new List<NominationProgram>();
            foreach (var category in userInput.CategoryId)
            {
                List<NominationProgram> categoryPrograms = _nominationProgram.Find<NominationProgram>(NominationProgram => NominationProgram.categoryId == category).ToList();
                programsForCategory.AddRange(categoryPrograms);
            }
            return programsForCategory;
        }
       

        public User GetUser(string userId) =>
       _users.Find<User>(User => User.UserId == userId).FirstOrDefault();

        private Random _random = new Random();

        public string GenerateRandomNo()
        {
            return _random.Next(0, 9999).ToString("D4");
        }
    }
}

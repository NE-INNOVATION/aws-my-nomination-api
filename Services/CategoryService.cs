using MongoDB.Driver;
using my_nomination_api.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_nomination_api.Services
{
    public class CategoryService
    {
        private readonly IMongoCollection<Category> _categories;

        public CategoryService(IMyNominationDatabaseSettings settings)
        {
            var client = new MongoClient(Environment.GetEnvironmentVariable("ConnectionString") ?? settings.ConnectionString);
            var database = client.GetDatabase(Environment.GetEnvironmentVariable("DatabaseName") ?? settings.DatabaseName);
            _categories = database.GetCollection<Category>(System.Environment.GetEnvironmentVariable("RegionCategoryCollectionName") ?? settings.RegionCategoryCollectionName);
        }

        public List<Category> GetAllCategories() =>
         _categories.Find(category => true).ToList();

        public List<Category> GetCategoriesByRegion(string regionId) =>
            _categories.Find<Category>(categories => categories.RegionId == regionId).ToList();

        public Category GetCategoriesById(string categoryId) =>
            _categories.Find<Category>(categories => categories.CategoryId == categoryId).FirstOrDefault();


        public Category CreateCategory(Category category)
        {
            _categories.InsertOne(category);
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            _categories.ReplaceOneAsync(b => b.CategoryId == category.CategoryId && b.RegionId == category.RegionId, category);
            return category;
        }


    }
}

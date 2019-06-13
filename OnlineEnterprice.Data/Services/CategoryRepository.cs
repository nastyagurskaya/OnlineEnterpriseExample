using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using OnlineEnterprice.Data.Settings;
using OnlineEnterprice.Domain.Entities;
using OnlineEnterprise.Data.Interfaces;

namespace OnlineEnterprise.Data.Services
{
    public class CategoryRepository : IMongoRepository<Category>
    {

        private readonly IMongoCollection<Category> _categories;

        public CategoryRepository(IShopDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _categories = database.GetCollection<Category>(settings.CollectionName);
        }

        public List<Category> Get() =>
            _categories.Find(category => true).ToList();

        public Category Get(string id) =>
            _categories.Find<Category>(category => category.Id == id).FirstOrDefault();

        public Category Create(Category category)
        {
            _categories.InsertOne(category);
            return category;
        }

        public void Update(string id, Category categoryIn) =>
            _categories.ReplaceOne(category => category.Id == id, categoryIn);

        public void Remove(Category categoryIn) =>
            _categories.DeleteOne(category => category.Id == categoryIn.Id);

        public void Remove(string id) =>
            _categories.DeleteOne(category => category.Id == id);
    }
}

using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration config) {

            var client = new MongoClient(config.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(config.GetValue<string>("DatabaseSettings:DatabaseName"));
            // it will create new collection if is not there
            Products = database.GetCollection<Product>(config.GetValue<string>(("DatabaseSettings:CollectionName")));

        }
        public IMongoCollection<Product> Products { get; }
    }
}

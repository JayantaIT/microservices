using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }

        public async Task CreateProduct(Product product)
        {
           if (product!= null)
            {
                await _catalogContext.Products.InsertOneAsync(product);

            }

        }
        public async Task<bool> UpdateProduct(Product product)
        {
            bool IsProductUpdated = false;
            var UpdateResult = await _catalogContext.Products.ReplaceOneAsync(filter:g=> g.Id == product.Id,replacement:product);
            IsProductUpdated = UpdateResult.IsAcknowledged && UpdateResult.ModifiedCount>0;
            return IsProductUpdated;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            bool IsProductDeleted = false;
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var DeleteResult = await _catalogContext.Products.DeleteOneAsync(filter);
            IsProductDeleted = DeleteResult.IsAcknowledged && DeleteResult.DeletedCount>0;
            return IsProductDeleted;
        }

        public async Task<Product> GetProduct(string id)
        {
            Product product = new Product();
            product = await   _catalogContext.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            IEnumerable<Product> products = new List<Product>();
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, category);
            products = await _catalogContext.Products.Find(filter).ToListAsync();
            return products;
           
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            IEnumerable<Product> products = new List<Product>();
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name,name);
            products = await _catalogContext.Products.Find(filter).ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            IEnumerable<Product> products = new List<Product>();
            products = await _catalogContext.Products.Find(p => true).ToListAsync();
            return products;
        }

       
    }
}

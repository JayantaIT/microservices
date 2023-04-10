using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(SetDummyData());
            }




        }

        private static IEnumerable<Product> SetDummyData()
        {
            List<Product> SetofProducts = new List<Product>()
            {
                new Product()
                {
                    Id="hgvkr652368",
                    Name="Redmi 9",
                    Summary="This is a phone",
                    Category=Category.Phone.ToString(),
                    Price=6543.87M,
                    Description="tfboih grhek"
                },
                new Product()
                {
                    Id="bjevs635673rvds",
                    Name="Zebronics Keyboard",
                    Description = "tfboih grhek",
                    Summary="This a electronics gadget",
                    Price =456.56M,
                    Category=Category.Gadget.ToString(),
                }
            };
            return SetofProducts;

        }
    }
}

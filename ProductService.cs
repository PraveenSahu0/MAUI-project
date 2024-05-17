namespace Pagination
{
    public class ProductService
    {
        private int totalProducts = 50; 
        private int pageSize = 10;

        public async Task<List<Product>> GetProductsAsync(int pageNumber)
        {
            await Task.Delay(1000);
            var products = Enumerable.Range((pageNumber - 1) * pageSize, pageSize)
                                     .Select(i => new Product
                                     {
                                         Id = i + 1,
                                         Name = $"Product {i + 1}",
                                         Description = $"Description for product {i + 1}"
                                     })
                                     .ToList();
            return products;
        }
    }

}

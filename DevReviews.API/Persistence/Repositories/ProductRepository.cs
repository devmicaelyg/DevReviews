using System.Collections.Generic;
using System.Threading.Tasks;
using DevReviews.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevReviews.API.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DevReviewsDbContext dbContext;

        public ProductRepository(DevReviewsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(Product product)
        {
            await this.dbContext.Products.AddAsync(product);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task AddReviewAsync(ProductReview productReview)
        {
            await this.dbContext.ProductsReview.AddAsync(productReview);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await this.dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
           return await this.dbContext.Products.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> GetDetailsByIdAsync(int id)
        {
           return await this.dbContext
                .Products
                .Include(product => product.Reviews)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ProductReview> GetReviewByIdAsync(int id)
        {
           return  await this.dbContext.ProductsReview.SingleOrDefaultAsync( p => p.Id  == id);
        }

        public async Task UpdateAsync(Product product)
        {
           this.dbContext.Products.Update(product);
           await this.dbContext.SaveChangesAsync();
        }
    }
}
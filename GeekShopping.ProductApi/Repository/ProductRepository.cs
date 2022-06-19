using AutoMapper;
using GeekShopping.ProductApi.Data.ValueObjects;
using GeekShopping.ProductApi.Model;
using GeekShopping.ProductApi.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySqlContext context;
        private readonly IMapper mapper;

        public ProductRepository(MySqlContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ProductVO> Create(ProductVO vo)
        {
            var productEntity = this.mapper.Map<Product>(vo);

            await this.context.AddAsync(productEntity);
            await this.context.SaveChangesAsync();

            var product = this.mapper.Map<ProductVO>(productEntity);

            return product;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var product = await this.context.Products.Where(p => p.Id == id)
                    .FirstOrDefaultAsync();
                if(product is null)
                    return false;

                this.context.Remove(product);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            

        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            var productsEntity = await this.context.Products
                .ToListAsync();
            var products = mapper.Map<List<ProductVO>>(productsEntity);

            return products;
        }

        public async Task<ProductVO> FindById(long id)
        {
            var productEntity = await this.context.Products
                .Where(p => p.Id == id).FirstOrDefaultAsync();

            var product = mapper.Map<ProductVO>(productEntity);

            return product;
        }

        public async Task<ProductVO> Update(ProductVO vo)
        {
            var productEntity = this.mapper.Map<Product>(vo);

            this.context.Update(productEntity);
            await this.context.SaveChangesAsync();

            var product = this.mapper.Map<ProductVO>(productEntity);

            return product;
        }
    }
}

using GeekShopping.ProductApi.Data.ValueObjects;
using GeekShopping.ProductApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(_productRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
            var products = await _productRepository.FindAll();

            if (products == null) return NotFound();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVO>> FindById(long id)
        {
            var product = await _productRepository.FindById(id);

            if(product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductVO>> Create([FromBody] ProductVO productVO)
        {
            if(productVO == null) return BadRequest();
            
            var product = await _productRepository.Create(productVO);

            return Ok(product);
        }

        [HttpPut]
        public async Task<ActionResult<ProductVO>> Update([FromBody] ProductVO productVO)
        {
            if (productVO == null) return BadRequest();

            var product = await _productRepository.Create(productVO);

            return Ok(product);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteById(long id)
        {
            var status = await _productRepository.Delete(id);
            if(status == false) return BadRequest();
            return Ok(status);
        }
    }
}

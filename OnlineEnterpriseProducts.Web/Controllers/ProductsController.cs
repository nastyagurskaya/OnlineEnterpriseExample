using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineEnterprice.Domain.Entities;
using OnlineEnterprise.Data.Interfaces;
using OnlineEnterpriseProducts.Web.ExternalApis;
using Refit;


namespace OnlineEnterprise.Web.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMongoRepository<Product> _productRepository;
        private ICategoryApi _categoryApi;

        public ProductsController(IMongoRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        //[HttpGet]
        //public IEnumerable<Product> Get() => _productRepository.Get();

        [HttpGet(Name = "GetProductsWithCategoryName")]
        public Dictionary<string, List<Product>> GetProductsWithCategoryName()
        {
            _categoryApi = RestService.For<ICategoryApi>("http://host.docker.internal:51492", new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(Request.Headers["Authorization"].ToString())
            });
            var products = _productRepository.Get().GroupBy(p => p.Category);
            return products.ToDictionary(cp => cp.Key == null ? String.Empty : _categoryApi.Get(cp.Key).Result.Name, cp => cp.ToList());
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public ActionResult<Product> Get(string id)
        {
            var product = _productRepository.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost(Name = "CreateProduct")]
        public ActionResult<Product> Create([FromBody]Product product)
        {
            _productRepository.Create(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id.ToString() }, product);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Product productIn)
        {
            var book = _productRepository.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _productRepository.Update(id, productIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var product = _productRepository.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            _productRepository.Remove(product.Id);

            return NoContent();
        }
    }
}

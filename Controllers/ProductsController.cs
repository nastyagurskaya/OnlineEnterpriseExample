using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OnlineEnterprice.Domain.Entities;
using OnlineEnterprise.Data.Interfaces;

namespace OnlineEnterpriseExmpl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IEnumerable<Product> Get() => _productRepository.Get();

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

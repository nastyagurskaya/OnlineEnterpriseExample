using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineEnterprice.Domain.Entities;
using OnlineEnterprise.Data.Interfaces;
using OnlineEnterprise.Data.Services;

namespace OnlineEnterprise.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMongoRepository<Category> _categoryRepository;

        public CategoryController(IMongoRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IEnumerable<Category> Get() => _categoryRepository.Get();

        [HttpGet("{id:length(24)}", Name = "GetCategory")]
        public ActionResult<Category> Get(string id)
        {
            var category = _categoryRepository.Get(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPost(Name = "CreateCategory")]
        public ActionResult<Order> Create([FromBody]Category category)
        {
            _categoryRepository.Create(category);

            return CreatedAtRoute("GetCategory", new { id = category.Id.ToString() }, category);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Category categoryIn)
        {
            var book = _categoryRepository.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _categoryRepository.Update(id, categoryIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var category = _categoryRepository.Get(id);

            if (category == null)
            {
                return NotFound();
            }

            _categoryRepository.Remove(category.Id);

            return NoContent();
        }
    }
}

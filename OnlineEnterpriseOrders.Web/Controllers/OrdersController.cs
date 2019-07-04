using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineEnterprice.Domain.Entities;
using OnlineEnterprise.Data.Interfaces;
using OnlineEnterprise.Data.Services;
using OnlineEnterpriseOrders.Web.ExternalApis;
using Refit;

namespace OnlineEnterprise.Web.Controllers
{
    //[Microsoft.AspNetCore.Authorization.Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMongoRepository<Order> _orderRepository;
        private readonly IProductsApi _productsApi;

        public OrdersController(IMongoRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
            _productsApi = RestService.For<IProductsApi>("http://host.docker.internal:51490");
        }

        //[HttpGet]
        //public IEnumerable<Order> Get() => _orderRepository.Get();

        [HttpGet(Name = "GetProductsWithCategoryName")]
        public Dictionary<Order, IEnumerable<Product>> GetProductsWithCategoryName()
        {
            Dictionary<Order, IEnumerable<Product>> result = new Dictionary<Order, IEnumerable<Product>>();
            var orders = _orderRepository.Get();
            orders.ForEach(o => result.Add(o, o.Products.Select(prId => _productsApi.Get(prId).Result)));
            return result;
        }

        [HttpGet("{id:length(24)}", Name = "GetOrder")]
        public ActionResult<Order> Get(string id)
        {
            var order = _orderRepository.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPost(Name = "CreateOrder")]
        public ActionResult<Order> Create([FromBody]Order order)
        {
            _orderRepository.Create(order);

            return CreatedAtRoute("GetOrder", new { id = order.Id.ToString() }, order);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Order orderIn)
        {
            var order = _orderRepository.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderRepository.Update(id, orderIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var order = _orderRepository.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderRepository.Remove(order.Id);

            return NoContent();
        }
    }
}

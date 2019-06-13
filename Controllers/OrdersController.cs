using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineEnterprice.Domain.Entities;
using OnlineEnterprise.Data.Services;

namespace OnlineEnterpriseExmpl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderRepository _orderRepository;

        public OrdersController(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public IEnumerable<Order> Get() => _orderRepository.Get();

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineEnterprice.Domain.Entities;
using Refit;

namespace OnlineEnterpriseOrders.Web.ExternalApis
{
    public interface IProductsApi
    {
        [Get("/api/Products/{id}")]
        Task<Product> Get(string id);
    }
}

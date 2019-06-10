using System;
using System.Collections.Generic;
using System.Text;
using OnlineEnterprice.Domain.Entities;

namespace OnlineEnterprise.Data.Interfaces
{
    public interface  IProductRepository
    {
        List<Product> Get();
        Product Get(string id);
        Product Create(Product product);
        void Update(string id, Product productIn);
        void Remove(Product productIn);
        void Remove(string id);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using OnlineEnterprice.Domain.Entities;

namespace OnlineEnterprise.Data.Interfaces
{
    public interface IMongoRepository<T>
    {
        List<T> Get();
        T Get(string id);
        T Create(T product);
        void Update(string id, T productIn);
        void Remove(T productIn);
        void Remove(string id);
    }
}

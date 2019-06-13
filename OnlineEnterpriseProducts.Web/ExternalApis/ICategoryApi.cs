﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineEnterprice.Domain.Entities;
using Refit;

namespace OnlineEnterpriseProducts.Web.ExternalApis
{
    public interface ICategoryApi
    {
        [Get("/api/Category/{id}")]
        Task<Category> Get(string id);
    }
}

using System.Threading.Tasks;
using OnlineEnterprice.Domain.Entities;
using Refit;

namespace OnlineEnterpriseProducts.Web.ExternalApis
{
    public interface ICategoryApi
    {
        [Get("/api/Category/{id}")]
        [Headers("Authorization: Bearer")]
        Task<Category> Get(string id);
    }
}

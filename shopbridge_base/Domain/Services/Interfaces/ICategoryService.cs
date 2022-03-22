using Shopbridge_base.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services.Interfaces
{
    public interface ICategoryService
    {
        bool IsExists(int id);
        Task<IEnumerable<Category>> GetAsync();
        Task<Category> GetAsync(int id);
        Task CreateAsync(Category entity);
        Task UpdateAsync(Category entity);
        Task DeleteAsync(int id);
    }
}

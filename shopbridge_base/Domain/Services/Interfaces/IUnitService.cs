using Shopbridge_base.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services.Interfaces
{
    public interface IUnitService
    {
        bool IsExists(int id);
        Task<IEnumerable<Unit>> GetAsync();
        Task<Unit> GetAsync(int id);
        Task CreateAsync(Unit entity);
        Task UpdateAsync(Unit entity);
        Task DeleteAsync(int id);
    }
}

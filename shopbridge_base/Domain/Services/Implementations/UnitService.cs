using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Data.Repository;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services
{
    public class UnitService : IUnitService
    {
        private readonly ILogger<UnitService> logger;
        private readonly IRepository repository;

        public UnitService(IRepository _repository)
        {
            repository = _repository;
        }
        public bool IsExists(int id)
        {
            return repository.Get<Unit>(u => u.UnitId == id).Any();
        }
        public async Task<IEnumerable<Unit>> GetAsync()
        {
            return await repository.AsQueryable<Unit>().ToListAsync();
        }
        public async Task<Unit> GetAsync(int id)
        {
            return await repository.Get<Unit>(u => u.UnitId == id).FirstOrDefaultAsync();
        }
        public async Task CreateAsync(Unit entity)
        {
            var isExists = repository.Get<Unit>(u => u.UnitName.ToLower() == entity.UnitName.ToLower()).Any();
            if (isExists)
            {
                throw new Exception(string.Format("Unit with {0} name already exists.!!!", entity.UnitName));
            }
            entity.UnitId = 0;
            await repository.CreateAsync<Unit>(entity);
        }
        public async Task UpdateAsync(Unit entity)
        {
            var isExists = repository.Get<Unit>(u => u.UnitId != entity.UnitId && u.UnitName.ToLower() == entity.UnitName.ToLower()).Any();
            if (isExists)
            {
                throw new Exception(string.Format("Unit with {0} name already exists.!!!", entity.UnitName));
            }
            await repository.UpdateAsync<Unit>(entity);
        }
        public async Task DeleteAsync(int id)
        {
            var entity = repository.Get<Unit>(u => u.UnitId == id).FirstOrDefault();
            if (entity == null)
            {
                throw new Exception(string.Format("Unit with {0} id Not Found.!!!", id));
            }

            var isUsed = repository.Get<Product>(c => c.UnitId == id).Any();
            if(isUsed)
            {
                throw new Exception(string.Format("Unit with {0} id is used in Product Master.!!!", id));
            }
            await repository.DeleteAsync<Unit>(entity);
        }
    }
}

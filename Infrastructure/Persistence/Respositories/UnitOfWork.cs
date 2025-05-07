using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistence.Data.Contexts;

namespace Persistence.Respositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {


        private readonly Dictionary<string, object> _repositories = [];
        


        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            // have a dictornay to store the repositories
            // where the key is the type of the entity and the value is the repository
        
            var typeName = typeof(TEntity).Name;
            if (_repositories.ContainsKey(typeName))
            {
                return (IGenericRepository<TEntity, TKey>)_repositories[typeName];
            }
            else
            {
                var repo = new GenericRespository<TEntity, TKey>(_dbContext);
                //_repositories.Add(typeName, repo);
                _repositories[typeName] = repo;
                return repo;
            }


        }

        public async Task<int> SaveChanges() => await _dbContext.SaveChangesAsync();
    }
}

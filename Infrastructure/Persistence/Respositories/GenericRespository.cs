using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;

namespace Persistence.Respositories
{
    public class GenericRespository<TEntity, TKey>(StoreDbContext _context) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _contextStoreDb = _context;


        public async Task AddAsync(TEntity entity) 
            => await _contextStoreDb.Set<TEntity>().AddAsync(entity);



        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _contextStoreDb.Set<TEntity>().ToListAsync();

        
        public async Task<TEntity?> GetByIdAsync(TKey Id) 
            => await _contextStoreDb.Set<TEntity>().FindAsync(Id);

       

        public void Update(TEntity entity) 
            => _contextStoreDb.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity)
            => _contextStoreDb.Set<TEntity>().Remove(entity);


        #region With Specifications


        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
           =>  await SpecificationEvaluator.CreateQuery<TEntity, TKey>(_contextStoreDb.Set<TEntity>(), specifications).ToListAsync();


        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
              => await SpecificationEvaluator.CreateQuery<TEntity, TKey>(_contextStoreDb.Set<TEntity>(), specifications).FirstOrDefaultAsync();

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
            => await SpecificationEvaluator.CreateQuery<TEntity, TKey>(InputQuery: _contextStoreDb.Set<TEntity>(),specifications).CountAsync();


        #endregion


    }
}

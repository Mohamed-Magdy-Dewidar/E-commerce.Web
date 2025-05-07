using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace DomainLayer.Contracts
{
    public interface IGenericRepository<TEntity , TKey > where TEntity : BaseEntity<TKey>
    {
        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        //Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector);

        //Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
        
        Task<IEnumerable<TEntity>> GetAllAsync();
        
        Task<TEntity?> GetByIdAsync(TKey Id);





    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;

namespace Service.Specifications
{
    abstract class BaseSpecification<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecification(Expression<Func<TEntity , bool>>? criteriaExpression)
        {
            Criteria = criteriaExpression;
        }

        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new List<Expression<Func<TEntity, object>>>();


        #region Include


        public void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            if (includeExpression == null)
                throw new ArgumentNullException(nameof(includeExpression));
            IncludeExpressions.Add(includeExpression);
        }

        #endregion


        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }


        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            if (orderByExpression == null)
                throw new ArgumentNullException(nameof(orderByExpression));
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        {
            if (orderByDescendingExpression == null)
                throw new ArgumentNullException(nameof(orderByDescendingExpression));
            OrderByDescending = orderByDescendingExpression;
        }

        #endregion



        #region Pagaination

        public int Skip  { get; private set; }

        public int Take { get; private set; }

        public bool IsPaginated { get; set; }


        //total Count = 40
        // pageSize = 10
        // pageIndex = 4
        // 10, 10 , 10 , 10
        protected void ApplyPagination(int PageSize , int PageIndex)
        {
            IsPaginated = true;
            if(PageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(PageSize), "Page size must be greater than zero.");
            Take = PageSize;
            if (PageIndex <= 0)
                throw new ArgumentOutOfRangeException(nameof(PageSize), "Page Index must be greater than zero.");

            Skip = (PageIndex - 1) * PageSize;
        }



        #endregion



    }
}

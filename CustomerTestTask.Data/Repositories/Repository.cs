using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerTestTask.Data.DynamicLinqItems;
using CustomerTestTask.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CustomerTestTask.Data.Repositories
{
    public class Repository<TEntity>: IRepository<TEntity> where TEntity: class
    {
        protected readonly Context _context;

        public Repository(Context context)
        {
            this._context = context;
        }

        public long Count(
            List<FilterItem<TEntity>> filterModels = null,
            List<OrderItem<TEntity>> sortModels = null
        )
        {
            return this.GetListQuery(0, 0, filterModels, sortModels).LongCount<TEntity>();                                                                        
        }

        public async Task<List<TEntity>> GetListAsync(
            int limit = 0, 
            int offset = 0, 
            List<FilterItem<TEntity>> filterModels = null, 
            List<OrderItem<TEntity>> sortModels = null
        )
        {    
            return await this.GetListQuery(limit, offset, filterModels, sortModels)
                            .ToListAsync<TEntity>(); 
        }

        private IQueryable<TEntity> GetListQuery (
            int limit = 0, 
            int offset = 0, 
            List<FilterItem<TEntity>> filterModels = null, 
            List<OrderItem<TEntity>> sortModels = null
        )
        {
            IQueryable<TEntity> query = this._context.Set<TEntity>().AsQueryable<TEntity>();                                
                    
            if (filterModels != null) 
            {
                foreach (FilterItem<TEntity> filterModel in filterModels) 
                {
                    query = filterModel.AddFilter(query);
                }
            } 

            if (sortModels != null) 
            {
                foreach (OrderItem<TEntity> sortModel in sortModels) 
                {
                    query = sortModel.AddOrder(query);
                }
            } 
             
            if (offset > 0) 
            {
                query = query.Skip<TEntity>(offset);
            }

            if (limit > 0) 
            {
                query = query.Take<TEntity>(limit);
            }

            return query;
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerTestTask.Data.DynamicLinqItems;

namespace CustomerTestTask.Data.IRepositories
{
    public interface IRepository<TEntity> where TEntity: class
    {
         long Count(
             List<FilterItem<TEntity>> filterModels = null,
             List<OrderItem<TEntity>> sortModels = null
         );
         
         Task<List<TEntity>> GetListAsync(
             int limit = 0,
             int offset = 0,
             List<FilterItem<TEntity>> filterModels = null,
             List<OrderItem<TEntity>> sortModels = null
         );        
    }
}
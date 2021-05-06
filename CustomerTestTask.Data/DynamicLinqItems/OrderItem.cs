using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace CustomerTestTask.Data.DynamicLinqItems
{
    public class OrderItem<TEntity> where TEntity: class
    {
        private List<string> _operatorList = new List<string>{"desc", "asc"}; 

        private string _customOperator;
        private string _name;

        public OrderItem(string name, string customOperator)
        {
            Name = name;
            CustomOperator = customOperator;        
        }

        public string Name {
            get => _name; 
            set {            
                if (typeof(TEntity).GetProperty(value) == null) 
                {
                    throw new Exception($"Invalid property name {value}");
                }
                _name = value;
            }
        }

        public string CustomOperator {
            get => _customOperator; 
            set {
                if (_operatorList.Any(o => o == value) == false)
                {
                   throw new Exception($"Invalid operator {value}");
                }
                _customOperator = value; 
            }
        }
                
        public IQueryable<TEntity> AddOrder(IQueryable<TEntity> query)
        {
            string str = $"{Name} {CustomOperator}";
            return query.Expression.Type == typeof(IOrderedQueryable<TEntity>)                 
                ? ((IOrderedQueryable<TEntity>) query).ThenBy(str) 
                : query.OrderBy(str);
        }   
    }
}
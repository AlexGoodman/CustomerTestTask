using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace CustomerTestTask.Data.DynamicLinqItems
{
    public class FilterItem<TEntity> where TEntity: class
    {
        private List<string> _operatorList = new List<string>{
            ">", "<", "==", "!=", ">=", "<=", "contains"
        };

        private string _customOperator;
        private string _name;

        public FilterItem(string name, string customOperator, string value)
        {
            Name = name;
            CustomOperator = customOperator;
            Value = value;
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

        public string Value {get; set;}
        
        public IQueryable<TEntity> AddFilter(IQueryable<TEntity> source)
        {
            if (CustomOperator == "contains") {
                return source.Where($"{Name}.Contains(@0)", Value);
            }

            return source.Where($"{Name} {CustomOperator} @0", Value);
        }
    }
}
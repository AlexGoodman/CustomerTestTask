using System.Collections.Generic;
using System.Text.Json.Serialization;
using CustomerTestTask.Data.DynamicLinqItems;
using Microsoft.AspNetCore.Mvc;

namespace CustomerTestTask.Api.ViewModels
{    
    public class ListRequestViewModel<TModel> where TModel: class
    {                
        public int Limit {get; set;} = 0;
                
        public int Offset {get; set;} = 0;
                
        public List<FilterItem<TModel>> FilterItems {get; set;} = null;
               
        public List<OrderItem<TModel>> OrderItems {get; set;} = null;         
    }
}
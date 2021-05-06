using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerTestTask.Api.ViewModels;
using CustomerTestTask.Data.DynamicLinqItems;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace CustomerTestTask.Api.ModelBinders
{
    public class ListRequestViewModelBinder<TModel>: IModelBinder where TModel: class
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {            
            string limit = bindingContext.ActionContext.HttpContext.Request.Query["limit"];
            string offset = bindingContext.ActionContext.HttpContext.Request.Query["offset"];
                                    
            ListRequestViewModel<TModel> model = new ListRequestViewModel<TModel>{
                Limit = limit != null ? int.Parse(limit) : 0,
                Offset = offset != null ? int.Parse(offset) : 0,            
                FilterItems = new List<FilterItem<TModel>>(),                                    
                OrderItems = new List<OrderItem<TModel>>(),                                    
            };

            foreach (string json in bindingContext.ActionContext.HttpContext.Request.Query["filterItems"].ToArray())
            {                
                model.FilterItems.Add(JsonConvert.DeserializeObject<FilterItem<TModel>>(json));
            }

            foreach (string json in bindingContext.ActionContext.HttpContext.Request.Query["orderItems"].ToArray())
            {                
                model.OrderItems.Add(JsonConvert.DeserializeObject<OrderItem<TModel>>(json));
            }

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}
using CustomerTestTask.Api.ModelBinders;
using CustomerTestTask.Api.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CustomerTestTask.Api.Providers
{
    public class ListRequestViewModelBinderProvider<TModel>: IModelBinderProvider where TModel: class
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(ListRequestViewModel<TModel>))
            {
                return new ListRequestViewModelBinder<TModel>();
            }                

            return null;
        }
    }
}
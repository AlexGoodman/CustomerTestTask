using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CustomerTestTask.Api.Resources
{
    public class ItemListResource<TModel> where TModel: class
    {
        [Required]            
        [JsonPropertyName("totalCount")]
        public long TotalCount {get; set;}

        [Required]            
        [JsonPropertyName("itemList")]
        public List<TModel> ItemList {get; set;}
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerTestTask.Api.Resources;
using CustomerTestTask.Api.ViewModels;
using CustomerTestTask.Data.DynamicLinqItems;
using CustomerTestTask.Data.Entities;
using CustomerTestTask.Data.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CustomerTestTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController: ControllerBase
    {
        
        private ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;    
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get customer list",
            Description = "Get customer list"            
        )]
        [ProducesResponseType(typeof(ItemListResource<CustomerEntity>), StatusCodes.Status200OK)] 
        [Route("list")]
        public async Task<IActionResult> List([FromQuery()] ListRequestViewModel<CustomerEntity> model)
        {                                        
            return Ok(new ItemListResource<CustomerEntity>{
                TotalCount = _customerRepository.Count(model.FilterItems, model.OrderItems),
                ItemList = await _customerRepository.GetListAsync(
                    model.Limit,
                    model.Offset,
                    model.FilterItems,
                    model.OrderItems
                ),
            });     
        }
    }
}
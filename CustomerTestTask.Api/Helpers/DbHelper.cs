using System;
using System.Linq;
using System.Threading.Tasks;
using CustomerTestTask.Data;
using CustomerTestTask.Data.Entities;

namespace CustomerTestTask.Api.Helpers
{
    public static class DbHelper
    {
        public static async Task Initialize(Context context)
        {
            if (context.Customers.Any() == true)
            {
                return;   
            }

            await context.Customers.AddAsync(new CustomerEntity{                
                Name = "Test",
                Age = 31
            });

            Random random = new Random();
            for(int i = 1; i < 1000; i++)
            {
               await context.Customers.AddAsync(new CustomerEntity{                
                   Name = Guid.NewGuid().ToString().Substring(0, 5),
                   Age = random.Next(1, 100)
               }); 
            }
            
            context.SaveChanges();
        }
    }
}
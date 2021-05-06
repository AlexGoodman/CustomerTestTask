using CustomerTestTask.Data.Entities;
using CustomerTestTask.Data.IRepositories;

namespace CustomerTestTask.Data.Repositories
{
    public class CustomerRepository : Repository<CustomerEntity>, ICustomerRepository
    {
        public CustomerRepository(Context context) : base(context)
        {
        }
    }
}
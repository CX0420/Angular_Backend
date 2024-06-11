using System.Collections.Generic;
using System.Threading.Tasks;
using AngularBackend.DataAccess;
using AngularBackend.Models;

namespace AngularBackend.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerDao _customerDao;

        public CustomerService(ICustomerDao customerDao)
        {
            _customerDao = customerDao;
        }

        public async Task<IEnumerable<CustomerModel>> GetAllCustomersAsync()
        {
            return await _customerDao.GetAllCustomersAsync();
        }

        public async Task<CustomerModel> GetCustomerByIdAsync(int id)
        {
            return await _customerDao.GetCustomerByIdAsync(id);
        }

        public async Task AddCustomerAsync(CustomerModel customer)
        {
            await _customerDao.AddCustomerAsync(customer);
        }

        public async Task UpdateCustomerAsync(CustomerModel customer)
        {
            await _customerDao.UpdateCustomerAsync(customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _customerDao.DeleteCustomerAsync(id);
        }

        public async Task<IEnumerable<CustomerModel>> SearchCustomerAsync(string keyword)
        {
            return await _customerDao.SearchCustomerAsync(keyword);
        }
    }
}

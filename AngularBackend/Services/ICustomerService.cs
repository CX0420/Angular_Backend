using System.Collections.Generic;
using System.Threading.Tasks;
using AngularBackend.Models;

namespace AngularBackend.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerModel>> GetAllCustomersAsync();
        Task<CustomerModel> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(CustomerModel customer);
        Task UpdateCustomerAsync(CustomerModel customer);
        Task DeleteCustomerAsync(int id);
        Task<IEnumerable<CustomerModel>> SearchCustomerAsync(string keyword);
    }
}

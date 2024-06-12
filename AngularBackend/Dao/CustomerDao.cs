//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using AngularBackend.Models;

//namespace AngularBackend.DataAccess
//{
//    public class CustomerDao : ICustomerDao
//    {
//        private readonly CustomerContext _context;

//        public CustomerDao(CustomerContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<CustomerModel>> GetAllCustomersAsync()
//        {
//            return await _context.Customer.Where(c => c.IsDeleted == 0).ToListAsync();
//        }

//        public async Task<CustomerModel> GetCustomerByIdAsync(int id)
//        {
//            return await _context.Customer.FirstOrDefaultAsync(c => c.CustomerId == id && c.IsDeleted == 0);
//        }

//        public async Task AddCustomerAsync(CustomerModel customer)
//        {
//            customer.CreatedTime = DateTime.Now;
//            customer.UpdatedTime = DateTime.Now;

//            _context.Customer.Add(customer);
//            await _context.SaveChangesAsync();
//        }

//        public async Task UpdateCustomerAsync(CustomerModel customer)
//        {
//            customer.UpdatedTime = DateTime.Now;

//            _context.Entry(customer).State = EntityState.Modified;
//            customer.UpdatedTime = DateTime.Now;
//            await _context.SaveChangesAsync();
//        }

//        public async Task DeleteCustomerAsync(int id)
//        {
//            var customer = await _context.Customer.FindAsync(id);
//            if (customer != null)
//            {
//                customer.IsDeleted = 1;
//                customer.UpdatedTime = DateTime.Now;
//                await _context.SaveChangesAsync();
//            }
//        }

//        public async Task<IEnumerable<CustomerModel>> SearchCustomerAsync(string keyword)
//        {
//            return await _context.Customer
//                .Where(c => c.IsDeleted == 0 &&
//                            (c.CustomerName.Contains(keyword) ||
//                             c.CustomerPhoneNumber.Contains(keyword) ||
//                             c.CustomerEmail.Contains(keyword)))
//                .ToListAsync();
//        }
//    }
//}


using AngularBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularBackend.DataAccess
{
    public class CustomerDao : ICustomerDao
    {
        private readonly CustomerContext _context;

        public CustomerDao(CustomerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerModel>> GetAllCustomersAsync()
        {
            return await _context.Customer.FromSqlRaw("SELECT * FROM Customer WHERE IsDeleted = 0").ToListAsync();
        }

        public async Task<CustomerModel> GetCustomerByIdAsync(int id)
        {
            return await _context.Customer.FromSqlRaw("SELECT * FROM Customer WHERE CustomerId = {0} AND IsDeleted = 0", id).FirstOrDefaultAsync();
        }

        public async Task AddCustomerAsync(CustomerModel customer)
        {
            await _context.Database.ExecuteSqlRawAsync("INSERT INTO Customer (CustomerName, CustomerEmail, CustomerPhoneNumber, CustomerGender) VALUES ({0}, {1}, {2}, {3})",
                customer.CustomerName, customer.CustomerEmail, customer.CustomerPhoneNumber, customer.CustomerGender);
        }

        public async Task UpdateCustomerAsync(CustomerModel customer)
        {
            await _context.Database.ExecuteSqlRawAsync("UPDATE Customer SET CustomerName = {0}, CustomerEmail = {1}, CustomerPhoneNumber = {2}, CustomerGender = {3}, UpdatedTime = CURRENT_TIMESTAMP WHERE CustomerId = {4} AND IsDeleted = 0",
                customer.CustomerName, customer.CustomerEmail, customer.CustomerPhoneNumber, customer.CustomerGender, customer.CustomerId);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("UPDATE Customer SET IsDeleted = 1, UpdatedTime = CURRENT_TIMESTAMP WHERE CustomerId = {0}", id);
        }

        public async Task<IEnumerable<CustomerModel>> SearchCustomerAsync(string keyword)
        {
            return await _context.Customer
                .FromSqlRaw("SELECT * FROM Customer WHERE IsDeleted = 0 AND CustomerName LIKE {0} OR CustomerPhoneNumber LIKE {0} OR CustomerEmail LIKE {0}", $"%{keyword}%")
                .ToListAsync();
        }
    }
}

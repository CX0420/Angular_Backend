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
//            return await _context.Customer.ToListAsync();
//        }

//        public async Task<CustomerModel> GetCustomerByIdAsync(int id)
//        {
//            return await _context.Customer.FindAsync(id);
//        }

//        public async Task AddCustomerAsync(CustomerModel customer)
//        {
//            _context.Customer.Add(customer);
//            await _context.SaveChangesAsync();
//        }

//        public async Task UpdateCustomerAsync(CustomerModel customer)
//        {
//            _context.Entry(customer).State = EntityState.Modified;
//            await _context.SaveChangesAsync();
//        }

//        public async Task DeleteCustomerAsync(int id)
//        {
//            var customer = await _context.Customer.FindAsync(id);
//            if (customer != null)
//            {
//                _context.Customer.Remove(customer);
//                await _context.SaveChangesAsync();
//            }
//        }
//    }
//}

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AngularBackend.Models;

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
            return await _context.Customer.FromSqlRaw("SELECT * FROM Customer").ToListAsync();
        }

        public async Task<CustomerModel> GetCustomerByIdAsync(int id)
        {
            return await _context.Customer.FromSqlRaw("SELECT * FROM Customer WHERE CustomerId = {0}", id).FirstOrDefaultAsync();
        }

        public async Task AddCustomerAsync(CustomerModel customer)
        {
            await _context.Database.ExecuteSqlRawAsync("INSERT INTO Customer (CustomerName, CustomerEmail, CustomerPhoneNumber, CustomerGender) VALUES ({0}, {1}, {2}, {3})",
                customer.CustomerName, customer.CustomerEmail, customer.CustomerPhoneNumber, customer.CustomerGender);
        }

        public async Task UpdateCustomerAsync(CustomerModel customer)
        {
            await _context.Database.ExecuteSqlRawAsync("UPDATE Customer SET CustomerName = {0}, CustomerEmail = {1}, CustomerPhoneNumber = {2}, CustomerGender = {3} WHERE CustomerId = {4}",
                customer.CustomerName, customer.CustomerEmail, customer.CustomerPhoneNumber, customer.CustomerGender, customer.CustomerId);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Customer WHERE CustomerId = {0}", id);
        }

        public async Task<IEnumerable<CustomerModel>> SearchCustomerAsync(string keyword)
        {
            return await _context.Customer
                .FromSqlRaw("SELECT * FROM Customer WHERE CustomerName LIKE {0} OR CustomerPhoneNumber LIKE {0} OR CustomerEmail LIKE {0}", $"%{keyword}%")
                .ToListAsync();
        }

    }
}

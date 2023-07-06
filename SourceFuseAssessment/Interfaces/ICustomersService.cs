using SourceFuseAssessment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SourceFuseAssessment.Interfaces
{
    public interface ICustomersService
    {
        Task<List<Customers>> GetCustomers();

        Task<Customers> GetCustomerById(string customerId);
    }
}

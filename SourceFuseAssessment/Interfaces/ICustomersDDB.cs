using Microsoft.Extensions.Configuration;
using SourceFuseAssessment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SourceFuseAssessment.Interfaces
{
    public interface ICustomersDDB
    {
        Task<Customers> GetCustomersById(IConfiguration config, string customerId);

        Task<List<Customers>> GetCustomers(IConfiguration config);
    }
}

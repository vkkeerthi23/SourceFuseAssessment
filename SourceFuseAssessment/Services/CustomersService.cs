using Microsoft.Extensions.Configuration;
using SourceFuseAssessment.DynamoDB;
using SourceFuseAssessment.Interfaces;
using SourceFuseAssessment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SourceFuseAssessment.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly IConfiguration _config;
       
        public CustomersService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<Customers>> GetCustomers() 
        {
            CustomersDDB dynamoDBCustomers = new CustomersDDB();
            return await dynamoDBCustomers.GetCustomers(_config);
        }

        public async Task<Customers> GetCustomerById(string customerId)
        {
            CustomersDDB dynamoDBCustomers = new CustomersDDB();
            return await dynamoDBCustomers.GetCustomersById(_config, customerId);
        }

    }
}

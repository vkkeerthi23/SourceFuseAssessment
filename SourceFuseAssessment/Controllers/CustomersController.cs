using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SourceFuseAssessment.Interfaces;
using SourceFuseAssessment.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SourceFuseAssessment.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class CustomersController : ControllerBase
    {   
        private readonly IConfiguration _configuration;
        private readonly ICustomersService _customerService;
        public CustomersController(IConfiguration configuration, ICustomersService customerService)
        {            
            _configuration = configuration;
            _customerService = customerService;
        }

        /// <summary>
        /// Get all Customers
        /// </summary>
        /// <returns>All Customers</returns>
        [HttpGet]
        public async Task<List<Customers>> Get()
        {
            try
            {
                List<Customers> lstCustomers = await _customerService.GetCustomers();
                return lstCustomers;
            }
            catch (Exception ex)
            {
                //Add Exception to logger;
                return null;
            }
        }

        /// <summary>
        /// Get Customer details By passing Id
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Customer Information</returns>
        [HttpPost]
        public async Task<Customers> GetCustomerById([FromBody] Customers customer)
        {
            try
            {
                Customers customerResponse = await _customerService.GetCustomerById(customer.CustomerId);
                return customerResponse;
            }
            catch (Exception ex)
            {
                //Add Exception to logger;
                return null;
            }
        }
    }
}
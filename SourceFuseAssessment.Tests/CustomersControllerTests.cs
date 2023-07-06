using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using SourceFuseAssessment.Controllers;
using SourceFuseAssessment.Interfaces;
using SourceFuseAssessment.Models;
using System.Collections.Generic;

namespace SourceFuseAssessment.Tests
{
    public class CustomerControllerTests
    {
        private Mock<IConfiguration> _configuration;
        private Mock<ICustomersService> _customersService;

        [SetUp]
        public void Setup()
        {
            _configuration = new Mock<IConfiguration>();
            _customersService = new Mock<ICustomersService>();
        }

        [Test]
        public void GetAllCustomers_Count_Success()
        {
            _customersService.Setup(m => m.GetCustomers()).ReturnsAsync(GetCustomers());
            CustomersController customer = new CustomersController(_configuration.Object, _customersService.Object);
            List<Customers> lstCustomers = customer.Get().Result;
            int actual = 2;
            int expected = lstCustomers.Count;
            NUnit.Framework.Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetAllCustomers_FirstRecord_Name_Success()
        {
            _customersService.Setup(m => m.GetCustomers()).ReturnsAsync(GetCustomers());
            CustomersController customer = new CustomersController(_configuration.Object, _customersService.Object);
            List<Customers> lstCustomers = customer.Get().Result;
            string actual = "Test1";
            string expected = lstCustomers[0].CustomerName;
            NUnit.Framework.Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetCustomerByID_Success()
        {
            Customers customer = GetCustomers()[0];
            _customersService.Setup(m => m.GetCustomerById("A01")).ReturnsAsync(customer);
            CustomersController customController = new CustomersController(_configuration.Object, _customersService.Object);
            Customers customerResponse = customController.GetCustomerById(customer).Result;
            string actual = customer.CustomerId;
            string expected = customerResponse.CustomerId;
            NUnit.Framework.Assert.That(actual, Is.EqualTo(expected));

        }

        private List<Customers> GetCustomers()
        {
            List<Customers> lstCustomer = new List<Customers>();
            Customers cust1 = new Customers();
            cust1.CustomerId = "A01";
            cust1.CustomerName = "Test1";
            lstCustomer.Add(cust1);

            Customers cust2 = new Customers();
            cust2.CustomerId = "A02";
            cust2.CustomerName = "Test2";
            lstCustomer.Add(cust2);
            return lstCustomer;
        }

    }
}
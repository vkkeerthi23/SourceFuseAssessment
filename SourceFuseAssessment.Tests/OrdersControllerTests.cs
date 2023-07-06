using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using SourceFuseAssessment.Controllers;
using SourceFuseAssessment.Interfaces;
using SourceFuseAssessment.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SourceFuseAssessment.Tests
{
    public class OrdersControllerTests
    {
        private Mock<IConfiguration> _configuration;
        private Mock<IOrdersService> _ordersService;

        [SetUp]
        public void Setup()
        {
            _configuration = new Mock<IConfiguration>();
            _ordersService = new Mock<IOrdersService>();
        }

        [Test]
        public void GetAllOrders_Count_Success()
        {
            _ordersService.Setup(m => m.GetOrders()).ReturnsAsync(GetOrders());
            OrdersController order = new OrdersController(_ordersService.Object);
            List<Orders> lstOrders = order.GetOrders().Result;
            int actual = 1;
            int expected = lstOrders.Count;
            NUnit.Framework.Assert.That(actual, Is.EqualTo(expected));
        }

        private List<Orders> GetOrders()
        {
            List<Orders> lstOrder = new List<Orders>();
            Orders ord1 = new Orders();
            ord1.OrderId = "A001";
            ord1.CustomerId = "100";
            lstOrder.Add(ord1);

            return lstOrder;
        }
    }
}

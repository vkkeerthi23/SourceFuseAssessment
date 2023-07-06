using Microsoft.Extensions.Configuration;
using SourceFuseAssessment.DynamoDB;
using SourceFuseAssessment.Interfaces;
using SourceFuseAssessment.Models;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace SourceFuseAssessment.Services
{
    public class OrdersService: IOrdersService
    {
        private readonly IConfiguration _config;

        public OrdersService(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Get All Orders
        /// </summary>
        /// <returns>All Orders</returns>
        public async Task<List<Orders>> GetOrders()
        {
            try
            {
                OrdersDDB dynamoDBOrders = new OrdersDDB();
                return await dynamoDBOrders.GetOrders(_config);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Orders by passing Customer ID
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>Orders related to Customer</returns>
        public async Task<Orders> GetOrdersByOrderId(string orderId)
        {
            try
            {
                OrdersDDB dynamoDBCustomers = new OrdersDDB();
                return await dynamoDBCustomers.GetOrdersByOrderId(_config, orderId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

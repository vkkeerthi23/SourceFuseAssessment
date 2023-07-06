using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SourceFuseAssessment.Interfaces;
using SourceFuseAssessment.Models;
using SourceFuseAssessment.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SourceFuseAssessment.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class OrdersController : ControllerBase
    {        
        private readonly IOrdersService _orderService;
        
        public OrdersController(IOrdersService orderService)
        {            
            _orderService = orderService;
        }

        /// <summary>
        /// Get All Orders
        /// </summary>
        /// <returns>All Orders</returns>
        [HttpGet]
        public async Task<List<Orders>> GetOrders()
        {
            try
            {
                List<Orders> lstOrders = await _orderService.GetOrders();
                return lstOrders;
            }
            catch(Exception ex)
            {
                //Add Exception to logger;
                return null;
            }
        }


        /// <summary>
        /// Get Orders by passing OrderId
        /// </summary>
        /// <param name="order"></param>
        /// <returns>Order</returns>
        [HttpPost]
        public async Task<Orders> GetOrdersByOrderId([FromBody] Orders order)
        {
            try
            {
                Orders orderResponse = await _orderService.GetOrdersByOrderId(order.OrderId);
                return orderResponse;
            }
            catch(Exception ex)
            {
                //Add Exception to logger;
                return null;
            }
        }
    }
}
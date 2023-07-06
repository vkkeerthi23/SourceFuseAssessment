using Microsoft.Extensions.Configuration;
using SourceFuseAssessment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SourceFuseAssessment.Interfaces
{
    public interface IOrdersDDB
    {
        Task<List<Orders>> GetOrders(IConfiguration config);

        Task<Orders> GetOrdersByOrderId(IConfiguration config, string orderId);
    }
}

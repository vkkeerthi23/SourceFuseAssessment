using SourceFuseAssessment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SourceFuseAssessment.Interfaces
{
    public interface IOrdersService
    {
        Task<List<Orders>> GetOrders();

        Task<Orders> GetOrdersByOrderId(string customerId);
    }
}

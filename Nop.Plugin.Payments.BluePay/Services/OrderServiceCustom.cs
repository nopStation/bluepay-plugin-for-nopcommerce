using System.Linq;
using System.Threading.Tasks;
using Nop.Core.Domain.Orders;
using Nop.Data;

namespace Nop.Plugin.Payments.BluePay.Services
{
    public class OrderServiceCustom : IOrderServiceCustom
    {
        #region Fields

        private readonly IRepository<Order> _orderRepository;

        #endregion

        #region Ctor

        public OrderServiceCustom(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        #endregion

        /// <summary>
        /// Send SMS 
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="orderId">Order id</param>
        /// <param name="settings">Clickatell settings</param>
        /// <returns>True if SMS was successfully sent; otherwise false</returns>
        public async Task<Order> GetOrderByAuthorizationTransactionIdAndPaymentMethodAsync(string authId, string systemName)
        {
            var query = from o in _orderRepository.Table
                        where o.AuthorizationTransactionId == authId && o.PaymentMethodSystemName == systemName
                        select o;
            return await query.FirstOrDefaultAsync();
        }
    }
}

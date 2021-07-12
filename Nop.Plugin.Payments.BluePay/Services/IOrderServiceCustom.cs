using System.Threading.Tasks;
using Nop.Core.Domain.Orders;

namespace Nop.Plugin.Payments.BluePay.Services
{
    public interface IOrderServiceCustom
    {
        Task<Order> GetOrderByAuthorizationTransactionIdAndPaymentMethodAsync(string authId, string systemName);
    }
}
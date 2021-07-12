using System;
using System.Threading.Tasks;
using Nop.Core.Domain.Orders;
using Nop.Core.Events;
using Nop.Services.Events;
using Nop.Services.Orders;

namespace Nop.Plugin.Payments.BluePay.Infrastructure.Cache
{
    /// <summary>
    /// RecurringPaymentInserted event consumer
    /// </summary>
    public partial class RecurringPaymentInsertedConsumer : IConsumer<EntityInsertedEvent<RecurringPayment>>
    {
        private readonly IOrderService _orderService;

        public RecurringPaymentInsertedConsumer(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="payment">The recurring payment.</param>
        public async Task HandleEventAsync(EntityInsertedEvent<RecurringPayment> payment)
        {
            var recurringPayment = payment.Entity;
            if (recurringPayment == null)
                return;

            var recurringPaymentHistory = await _orderService.SearchRecurringPaymentsAsync(initialOrderId: payment.Entity.Id);
            var order = await _orderService.GetOrderByIdAsync(recurringPayment.InitialOrderId);
            //first payment already was paid on the BluePay, let's add it to history
            if (recurringPaymentHistory.Count == 0 && order.PaymentMethodSystemName == "Payments.BluePay")
            {
                await _orderService.InsertRecurringPaymentHistoryAsync(new RecurringPaymentHistory
                {
                    RecurringPaymentId = recurringPayment.Id,
                    OrderId = recurringPayment.InitialOrderId,
                    CreatedOnUtc = DateTime.UtcNow
                });
            }
        }
    }
}

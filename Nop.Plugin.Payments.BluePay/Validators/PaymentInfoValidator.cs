using FluentValidation;
using Nop.Plugin.Payments.BluePay.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Payments.BluePay.Validators
{
    public class PaymentInfoValidator : BaseNopValidator<PaymentInfoModel>
    {
        public PaymentInfoValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.CardNumber).IsCreditCard().WithMessage(localizationService.GetResourceAsync("Payment.CardNumber.Wrong").Result);
            RuleFor(x => x.ExpireMonth).NotEmpty().WithMessage(localizationService.GetResourceAsync("Payment.ExpireMonth.Required").Result);
            RuleFor(x => x.ExpireYear).NotEmpty().WithMessage(localizationService.GetResourceAsync("Payment.ExpireYear.Required").Result);
            RuleFor(x => x.CardCode).Matches(@"^[0-9]{3,4}$").WithMessage(localizationService.GetResourceAsync("Payment.CardCode.Wrong").Result);
        }
    }
}
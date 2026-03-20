using PaymentProcessingSystem.Domain.Enums;

namespace PaymentProcessingSystem.Domain.Models;

public class PayPalPaymentDetails : PaymentDetailsBase
{
    public override PaymentMethodType MethodType => PaymentMethodType.PayPal;

    public string Email { get; set; } = string.Empty;
}
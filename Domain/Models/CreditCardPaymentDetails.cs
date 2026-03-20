using PaymentProcessingSystem.Domain.Enums;

namespace PaymentProcessingSystem.Domain.Models;

public class CreditCardPaymentDetails : PaymentDetailsBase
{
    public override PaymentMethodType MethodType => PaymentMethodType.CreditCard;

    public string CardHolderName { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public string CVV { get; set; } = string.Empty;
}
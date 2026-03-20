using PaymentProcessingSystem.Domain.Enums;

namespace PaymentProcessingSystem.Domain.Models;

public class PaymentRequest
{
    public decimal Amount { get; set; }
    public CurrencyType Currency { get; set; }
    public PaymentMethodType PaymentMethodType { get; set; }
    public string Description { get; set; } = string.Empty;
    public string CustomerReference { get; set; } = string.Empty;
    public PaymentDetailsBase PaymentDetails { get; set; } = default!;
}
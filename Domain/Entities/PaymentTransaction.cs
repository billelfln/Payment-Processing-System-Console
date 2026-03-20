using PaymentProcessingSystem.Domain.Enums;

namespace PaymentProcessingSystem.Domain.Entities;

public class PaymentTransaction
{
    public string Id { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal Fee { get; set; }
    public decimal NetAmount { get; set; }

    public CurrencyType Currency { get; set; }
    public PaymentMethodType PaymentMethodType { get; set; }
    public PaymentStatus Status { get; set; }

    public string Description { get; set; } = string.Empty;
    public string CustomerReference { get; set; } = string.Empty;
    public string FailureReason { get; set; } = string.Empty;

    public bool IsRefunded { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public DateTime? RefundedAt { get; set; }

    public bool FraudFlagged { get; set; }
}
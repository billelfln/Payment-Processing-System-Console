using PaymentProcessingSystem.Domain.Enums;

namespace PaymentProcessingSystem.Domain.Models;

public abstract class PaymentDetailsBase
{
    public abstract PaymentMethodType MethodType { get; }
}
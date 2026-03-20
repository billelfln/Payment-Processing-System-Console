using PaymentProcessingSystem.Domain.Enums;
using PaymentProcessingSystem.Domain.Interfaces;

namespace PaymentProcessingSystem.Infrastructure.FeeStrategies;

public class PayPalFeeStrategy : IFeeStrategy
{
    public PaymentMethodType MethodType => PaymentMethodType.PayPal;

    public decimal CalculateFee(decimal amount)
    {
        return amount * 0.03m; // 3%
    }
}
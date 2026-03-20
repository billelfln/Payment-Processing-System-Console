using PaymentProcessingSystem.Domain.Enums;
using PaymentProcessingSystem.Domain.Interfaces;

namespace PaymentProcessingSystem.Infrastructure.FeeStrategies;

public class CreditCardFeeStrategy : IFeeStrategy
{
    public PaymentMethodType MethodType => PaymentMethodType.CreditCard;

    public decimal CalculateFee(decimal amount)
    {
        return amount * 0.025m + 1m; // 2.5% + 1
    }
}
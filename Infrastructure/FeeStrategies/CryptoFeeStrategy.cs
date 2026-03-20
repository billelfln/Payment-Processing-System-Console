using PaymentProcessingSystem.Domain.Enums;
using PaymentProcessingSystem.Domain.Interfaces;

namespace PaymentProcessingSystem.Infrastructure.FeeStrategies;

public class CryptoFeeStrategy : IFeeStrategy
{
    public PaymentMethodType MethodType => PaymentMethodType.Crypto;

    public decimal CalculateFee(decimal amount)
    {
        return amount * 0.018m; // 1.8%
    }
}
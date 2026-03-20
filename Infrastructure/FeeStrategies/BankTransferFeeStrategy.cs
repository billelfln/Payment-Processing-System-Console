using PaymentProcessingSystem.Domain.Enums;
using PaymentProcessingSystem.Domain.Interfaces;

namespace PaymentProcessingSystem.Infrastructure.FeeStrategies;

public class BankTransferFeeStrategy : IFeeStrategy
{
    public PaymentMethodType MethodType => PaymentMethodType.BankTransfer;

    public decimal CalculateFee(decimal amount)
    {
        return amount * 0.01m; // 1%
    }
}
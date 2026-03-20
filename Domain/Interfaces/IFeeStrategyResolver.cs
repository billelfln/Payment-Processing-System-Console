using PaymentProcessingSystem.Domain.Enums;

namespace PaymentProcessingSystem.Domain.Interfaces;

public interface IFeeStrategyResolver
{
    IFeeStrategy Resolve(PaymentMethodType methodType);
}
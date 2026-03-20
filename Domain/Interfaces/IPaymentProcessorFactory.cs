using PaymentProcessingSystem.Domain.Enums;

namespace PaymentProcessingSystem.Domain.Interfaces;

public interface IPaymentProcessorFactory
{
    IPaymentProcessor Create(PaymentMethodType methodType);
}
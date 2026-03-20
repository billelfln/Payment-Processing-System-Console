using PaymentProcessingSystem.Domain.Enums;
using PaymentProcessingSystem.Domain.Interfaces;

namespace PaymentProcessingSystem.Application.Factories;

public class PaymentProcessorFactory : IPaymentProcessorFactory
{
    private readonly IEnumerable<IPaymentProcessor> _processors;

    public PaymentProcessorFactory(IEnumerable<IPaymentProcessor> processors)
    {
        _processors = processors;
    }

    public IPaymentProcessor Create(PaymentMethodType methodType)
    {
        var processor = _processors.FirstOrDefault(p => p.MethodType == methodType);

        if (processor is null)
            throw new InvalidOperationException($"No payment processor found for method: {methodType}");

        return processor;
    }
}
using PaymentProcessingSystem.Application.Results;
using PaymentProcessingSystem.Domain.Models;
using PaymentProcessingSystem.Domain.Enums;

namespace PaymentProcessingSystem.Domain.Interfaces;

public interface IPaymentProcessor
{
    PaymentMethodType MethodType { get; }
    OperationResult Process(PaymentRequest request);
}
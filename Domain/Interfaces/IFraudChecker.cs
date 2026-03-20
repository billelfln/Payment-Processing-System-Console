using PaymentProcessingSystem.Application.Results;
using PaymentProcessingSystem.Domain.Models;

namespace PaymentProcessingSystem.Domain.Interfaces;

public interface IFraudChecker
{
    OperationResult Check(PaymentRequest request);
}
using PaymentProcessingSystem.Application.Results;
using PaymentProcessingSystem.Domain.Enums;
using PaymentProcessingSystem.Domain.Interfaces;
using PaymentProcessingSystem.Domain.Models;

namespace PaymentProcessingSystem.Infrastructure.Processors;

public class BankTransferPaymentProcessor : IPaymentProcessor
{
    public PaymentMethodType MethodType => PaymentMethodType.BankTransfer;

    public OperationResult Process(PaymentRequest request)
    {
        if (request.PaymentDetails is not BankTransferPaymentDetails details)
            return OperationResult.Failure("Invalid bank transfer details.");

        if (string.IsNullOrWhiteSpace(details.IBAN))
            return OperationResult.Failure("IBAN is required.");

        if (string.IsNullOrWhiteSpace(details.BankName))
            return OperationResult.Failure("Bank name is required.");

        return OperationResult.Success("Bank transfer processed successfully.");
    }
}
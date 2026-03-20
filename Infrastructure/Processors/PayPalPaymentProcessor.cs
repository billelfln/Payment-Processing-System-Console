using PaymentProcessingSystem.Application.Results;
using PaymentProcessingSystem.Domain.Enums;
using PaymentProcessingSystem.Domain.Interfaces;
using PaymentProcessingSystem.Domain.Models;

namespace PaymentProcessingSystem.Infrastructure.Processors;

public class PayPalPaymentProcessor : IPaymentProcessor
{
    public PaymentMethodType MethodType => PaymentMethodType.PayPal;

    public OperationResult Process(PaymentRequest request)
    {
        if (request.PaymentDetails is not PayPalPaymentDetails details)
            return OperationResult.Failure("Invalid PayPal payment details.");

        if (string.IsNullOrWhiteSpace(details.Email) || !details.Email.Contains("@"))
            return OperationResult.Failure("Invalid PayPal email.");

        return OperationResult.Success("PayPal payment processed successfully.");
    }
}
using PaymentProcessingSystem.Application.Results;
using PaymentProcessingSystem.Domain.Enums;
using PaymentProcessingSystem.Domain.Interfaces;
using PaymentProcessingSystem.Domain.Models;

namespace PaymentProcessingSystem.Infrastructure.Processors;

public class CreditCardPaymentProcessor : IPaymentProcessor
{
    public PaymentMethodType MethodType => PaymentMethodType.CreditCard;

    public OperationResult Process(PaymentRequest request)
    {
        if (request.PaymentDetails is not CreditCardPaymentDetails details)
            return OperationResult.Failure("Invalid credit card payment details.");

        if (string.IsNullOrWhiteSpace(details.CardNumber) || details.CardNumber.Length < 12)
            return OperationResult.Failure("Invalid card number.");

        if (string.IsNullOrWhiteSpace(details.CVV) || details.CVV.Length < 3)
            return OperationResult.Failure("Invalid CVV.");

        return OperationResult.Success("Credit card payment processed successfully.");
    }
}
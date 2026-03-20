using PaymentProcessingSystem.Application.Results;
using PaymentProcessingSystem.Domain.Enums;
using PaymentProcessingSystem.Domain.Interfaces;
using PaymentProcessingSystem.Domain.Models;

namespace PaymentProcessingSystem.Infrastructure.Processors;

public class CryptoPaymentProcessor : IPaymentProcessor
{
    public PaymentMethodType MethodType => PaymentMethodType.Crypto;

    public OperationResult Process(PaymentRequest request)
    {
        if (request.PaymentDetails is not CryptoPaymentDetails details)
            return OperationResult.Failure("Invalid crypto payment details.");

        if (string.IsNullOrWhiteSpace(details.WalletAddress))
            return OperationResult.Failure("Wallet address is required.");

        if (string.IsNullOrWhiteSpace(details.Network))
            return OperationResult.Failure("Crypto network is required.");

        return OperationResult.Success("Crypto payment processed successfully.");
    }
}
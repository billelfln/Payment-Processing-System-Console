using PaymentProcessingSystem.Application.Results;
using PaymentProcessingSystem.Domain.Enums;
using PaymentProcessingSystem.Domain.Interfaces;
using PaymentProcessingSystem.Domain.Models;

namespace PaymentProcessingSystem.Infrastructure.Fraud;

public class MockFraudChecker : IFraudChecker
{
    public OperationResult Check(PaymentRequest request)
    {
        if (request.Amount > 10000)
        {
            return OperationResult.Failure("High amount detected - possible fraud.");
        }

        if (request.PaymentMethodType == PaymentMethodType.Crypto && request.Amount > 5000)
        {
            return OperationResult.Failure("Crypto high amount flagged.");
        }

        if (request.PaymentDetails is PayPalPaymentDetails paypal)
        {
            if (paypal.Email.Contains("fraud"))
            {
                return OperationResult.Failure("Blacklisted PayPal account.");
            }
        }

        return OperationResult.Success("Fraud check passed.");
    }
}

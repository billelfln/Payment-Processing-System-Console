using PaymentProcessingSystem.Application.Results;
using PaymentProcessingSystem.Domain.Enums;
using PaymentProcessingSystem.Domain.Interfaces;

namespace PaymentProcessingSystem.Application.Services;

public class RefundService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IAppLogger _logger;

    public RefundService(IPaymentRepository paymentRepository, IAppLogger logger)
    {
        _paymentRepository = paymentRepository;
        _logger = logger;
    }

    public OperationResult Refund(string transactionId)
    {
        _logger.LogInfo($"Refund requested for transaction: {transactionId}");

        var transaction = _paymentRepository.GetById(transactionId);
        if (transaction is null)
        {
            _logger.LogError("Transaction not found.");
            return OperationResult.Failure("Transaction not found.");
        }

        if (transaction.Status != PaymentStatus.Succeeded)
        {
            _logger.LogWarning("Only succeeded transactions can be refunded.");
            return OperationResult.Failure("Only succeeded transactions can be refunded.");
        }

        if (transaction.IsRefunded)
        {
            _logger.LogWarning("Transaction has already been refunded.");
            return OperationResult.Failure("Transaction has already been refunded.");
        }

        if (transaction.PaymentMethodType == PaymentMethodType.Crypto)
        {
            _logger.LogWarning("Crypto payments cannot be refunded.");
            return OperationResult.Failure("Crypto payments cannot be refunded.");
        }

        transaction.IsRefunded = true;
        transaction.RefundedAt = DateTime.Now;
        transaction.Status = PaymentStatus.Refunded;

        _paymentRepository.Update(transaction);

        _logger.LogInfo($"Refund completed for transaction: {transaction.Id}");
        return OperationResult.Success("Refund completed successfully.");
    }
}
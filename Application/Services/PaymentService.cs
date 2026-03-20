using PaymentProcessingSystem.Application.Results;
using PaymentProcessingSystem.Domain.Entities;
using PaymentProcessingSystem.Domain.Enums;
using PaymentProcessingSystem.Domain.Interfaces;
using PaymentProcessingSystem.Domain.Models;

namespace PaymentProcessingSystem.Application.Services;

public class PaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentProcessorFactory _paymentProcessorFactory;
    private readonly IAppLogger _logger;
    private readonly IFraudChecker _fraudChecker;
    private readonly IFeeStrategyResolver _feeStrategyResolver;
    private readonly ITransactionIdGenerator _transactionIdGenerator;

    public PaymentService(
        IPaymentRepository paymentRepository,
        IPaymentProcessorFactory paymentProcessorFactory,
        IAppLogger logger,
        IFraudChecker fraudChecker,
        IFeeStrategyResolver feeStrategyResolver,
        ITransactionIdGenerator transactionIdGenerator)
    {
        _paymentRepository = paymentRepository;
        _paymentProcessorFactory = paymentProcessorFactory;
        _logger = logger;
        _fraudChecker = fraudChecker;
        _feeStrategyResolver = feeStrategyResolver;
        _transactionIdGenerator = transactionIdGenerator;
    }

    public OperationResult<PaymentTransaction> ProcessPayment(PaymentRequest request)
    {
        _logger.LogInfo($"Starting payment processing for method: {request.PaymentMethodType}");

        if (request.Amount <= 0)
        {
            _logger.LogError("Payment amount must be greater than zero.");
            return OperationResult<PaymentTransaction>.Failure("Payment amount must be greater than zero.");
        }

        if (request.PaymentDetails is null)
        {
            _logger.LogError("Payment details are required.");
            return OperationResult<PaymentTransaction>.Failure("Payment details are required.");
        }

        var fraudResult = _fraudChecker.Check(request);
        if (!fraudResult.IsSuccess)
        {
            _logger.LogWarning($"Fraud check failed: {fraudResult.Message}");

            var failedTransaction = new PaymentTransaction
            {
                Id = _transactionIdGenerator.Generate(),
                Amount = request.Amount,
                Currency = request.Currency,
                PaymentMethodType = request.PaymentMethodType,
                Status = PaymentStatus.Failed,
                Description = request.Description,
                CustomerReference = request.CustomerReference,
                FailureReason = fraudResult.Message,
                CreatedAt = DateTime.Now,
                ProcessedAt = DateTime.Now,
                FraudFlagged = true
            };

            _paymentRepository.Add(failedTransaction);

            return OperationResult<PaymentTransaction>.Failure(fraudResult.Message);
        }

        var feeStrategy = _feeStrategyResolver.Resolve(request.PaymentMethodType);
        var fee = feeStrategy.CalculateFee(request.Amount);

        var processor = _paymentProcessorFactory.Create(request.PaymentMethodType);
        var processingResult = processor.Process(request);

        var transaction = new PaymentTransaction
        {
            Id = _transactionIdGenerator.Generate(),
            Amount = request.Amount,
            Fee = fee,
            NetAmount = request.Amount - fee,
            Currency = request.Currency,
            PaymentMethodType = request.PaymentMethodType,
            Status = processingResult.IsSuccess ? PaymentStatus.Succeeded : PaymentStatus.Failed,
            Description = request.Description,
            CustomerReference = request.CustomerReference,
            FailureReason = processingResult.IsSuccess ? string.Empty : processingResult.Message,
            CreatedAt = DateTime.Now,
            ProcessedAt = DateTime.Now,
            FraudFlagged = false
        };

        _paymentRepository.Add(transaction);

        if (processingResult.IsSuccess)
        {
            _logger.LogInfo($"Payment succeeded. Transaction ID: {transaction.Id}");
            return OperationResult<PaymentTransaction>.Success("Payment processed successfully.", transaction);
        }

        _logger.LogError($"Payment failed. Reason: {processingResult.Message}");
        return OperationResult<PaymentTransaction>.Failure(processingResult.Message);
    }
}
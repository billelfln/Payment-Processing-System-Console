using PaymentProcessingSystem.Domain.Entities;
using PaymentProcessingSystem.Domain.Interfaces;

namespace PaymentProcessingSystem.Application.Services;

public class TransactionQueryService
{
    private readonly IPaymentRepository _paymentRepository;

    public TransactionQueryService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public PaymentTransaction? GetById(string transactionId)
    {
        return _paymentRepository.GetById(transactionId);
    }

    public IEnumerable<PaymentTransaction> GetAll()
    {
        return _paymentRepository.GetAll();
    }
}
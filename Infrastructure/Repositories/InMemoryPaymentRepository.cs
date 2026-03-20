using PaymentProcessingSystem.Domain.Entities;
using PaymentProcessingSystem.Domain.Interfaces;

namespace PaymentProcessingSystem.Infrastructure.Repositories;

public class InMemoryPaymentRepository : IPaymentRepository
{
    private readonly List<PaymentTransaction> _transactions = new();

    public void Add(PaymentTransaction transaction)
    {
        _transactions.Add(transaction);
    }

    public void Update(PaymentTransaction transaction)
    {
        var existing = _transactions.FirstOrDefault(t => t.Id == transaction.Id);
        if (existing != null)
        {
            _transactions.Remove(existing);
            _transactions.Add(transaction);
        }
    }

    public PaymentTransaction? GetById(string id)
    {
        return _transactions.FirstOrDefault(t => t.Id == id);
    }

    public IEnumerable<PaymentTransaction> GetAll()
    {
        return _transactions;
    }
}
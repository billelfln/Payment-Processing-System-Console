using PaymentProcessingSystem.Domain.Entities;

namespace PaymentProcessingSystem.Domain.Interfaces;

public interface IPaymentRepository
{
    void Add(PaymentTransaction transaction);
    void Update(PaymentTransaction transaction);
    PaymentTransaction? GetById(string id);
    IEnumerable<PaymentTransaction> GetAll();
}
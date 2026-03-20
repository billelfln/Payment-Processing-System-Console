using PaymentProcessingSystem.Domain.Interfaces;

namespace PaymentProcessingSystem.Infrastructure.Generators;

public class TransactionIdGenerator : ITransactionIdGenerator
{
    private int _counter = 1;

    public string Generate()
    {
        var date = DateTime.Now.ToString("yyyyMMdd");
        var id = $"TXN-{date}-{_counter:D4}";
        _counter++;
        return id;
    }
}
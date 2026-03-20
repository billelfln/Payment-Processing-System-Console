namespace PaymentProcessingSystem.Domain.Interfaces;

public interface ITransactionIdGenerator
{
    string Generate();
}
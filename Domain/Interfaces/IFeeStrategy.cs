using PaymentProcessingSystem.Domain.Enums;


public interface IFeeStrategy
{
    PaymentMethodType MethodType { get; }
    decimal CalculateFee(decimal amount);
}
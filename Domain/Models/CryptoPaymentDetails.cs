using PaymentProcessingSystem.Domain.Enums;

namespace PaymentProcessingSystem.Domain.Models;

public class CryptoPaymentDetails : PaymentDetailsBase
{
    public override PaymentMethodType MethodType => PaymentMethodType.Crypto;

    public string WalletAddress { get; set; } = string.Empty;
    public string Network { get; set; } = string.Empty;
}
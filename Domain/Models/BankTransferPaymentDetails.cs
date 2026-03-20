using PaymentProcessingSystem.Domain.Enums;

namespace PaymentProcessingSystem.Domain.Models;

public class BankTransferPaymentDetails : PaymentDetailsBase
{
    public override PaymentMethodType MethodType => PaymentMethodType.BankTransfer;

    public string AccountHolderName { get; set; } = string.Empty;
    public string BankName { get; set; } = string.Empty;
    public string IBAN { get; set; } = string.Empty;
}
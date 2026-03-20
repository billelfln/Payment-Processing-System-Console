using PaymentProcessingSystem.Domain.Enums;
using PaymentProcessingSystem.Domain.Models;
using PaymentProcessingSystem.Presentation.Output;

namespace PaymentProcessingSystem.Presentation.InputHandlers;

public class PaymentInputHandler
{
    public PaymentRequest ReadPaymentRequest()
    {
        ConsoleHelper.WriteSection("Payment Request");

        var amount = ConsoleHelper.ReadDecimal("Enter amount");
        var currency = ReadCurrency();
        var methodType = ReadPaymentMethod();
        var description = ConsoleHelper.ReadRequiredString("Enter description");
        var customerReference = ConsoleHelper.ReadRequiredString("Enter customer reference");

        var paymentDetails = ReadPaymentDetails(methodType);

        return new PaymentRequest
        {
            Amount = amount,
            Currency = currency,
            PaymentMethodType = methodType,
            Description = description,
            CustomerReference = customerReference,
            PaymentDetails = paymentDetails
        };
    }

    private CurrencyType ReadCurrency()
    {
        while (true)
        {
            ConsoleHelper.WriteInfo("Choose currency:");
            Console.WriteLine("1. USD");
            Console.WriteLine("2. EUR");
            Console.WriteLine("3. DZD");

            var choice = ConsoleHelper.ReadInt("Your choice");

            return choice switch
            {
                1 => CurrencyType.USD,
                2 => CurrencyType.EUR,
                3 => CurrencyType.DZD,
                _ => ShowInvalidCurrency()
            };
        }
    }

    private CurrencyType ShowInvalidCurrency()
    {
        ConsoleHelper.WriteWarning("Invalid currency choice.");
        return ReadCurrency();
    }

    private PaymentMethodType ReadPaymentMethod()
    {
        while (true)
        {
            ConsoleHelper.WriteInfo("Choose payment method:");
            Console.WriteLine("1. Credit Card");
            Console.WriteLine("2. PayPal");
            Console.WriteLine("3. Bank Transfer");
            Console.WriteLine("4. Crypto");

            var choice = ConsoleHelper.ReadInt("Your choice");

            return choice switch
            {
                1 => PaymentMethodType.CreditCard,
                2 => PaymentMethodType.PayPal,
                3 => PaymentMethodType.BankTransfer,
                4 => PaymentMethodType.Crypto,
                _ => ShowInvalidPaymentMethod()
            };
        }
    }

    private PaymentMethodType ShowInvalidPaymentMethod()
    {
        ConsoleHelper.WriteWarning("Invalid payment method choice.");
        return ReadPaymentMethod();
    }

    private PaymentDetailsBase ReadPaymentDetails(PaymentMethodType methodType)
    {
        return methodType switch
        {
            PaymentMethodType.CreditCard => ReadCreditCardDetails(),
            PaymentMethodType.PayPal => ReadPayPalDetails(),
            PaymentMethodType.BankTransfer => ReadBankTransferDetails(),
            PaymentMethodType.Crypto => ReadCryptoDetails(),
            _ => throw new InvalidOperationException("Unsupported payment method.")
        };
    }

    private CreditCardPaymentDetails ReadCreditCardDetails()
    {
        ConsoleHelper.WriteSection("Credit Card Details");

        return new CreditCardPaymentDetails
        {
            CardHolderName = ConsoleHelper.ReadRequiredString("Card holder name"),
            CardNumber = ConsoleHelper.ReadRequiredString("Card number"),
            ExpiryMonth = ConsoleHelper.ReadInt("Expiry month"),
            ExpiryYear = ConsoleHelper.ReadInt("Expiry year"),
            CVV = ConsoleHelper.ReadRequiredString("CVV")
        };
    }

    private PayPalPaymentDetails ReadPayPalDetails()
    {
        ConsoleHelper.WriteSection("PayPal Details");

        return new PayPalPaymentDetails
        {
            Email = ConsoleHelper.ReadRequiredString("PayPal email")
        };
    }

    private BankTransferPaymentDetails ReadBankTransferDetails()
    {
        ConsoleHelper.WriteSection("Bank Transfer Details");

        return new BankTransferPaymentDetails
        {
            AccountHolderName = ConsoleHelper.ReadRequiredString("Account holder name"),
            BankName = ConsoleHelper.ReadRequiredString("Bank name"),
            IBAN = ConsoleHelper.ReadRequiredString("IBAN")
        };
    }

    private CryptoPaymentDetails ReadCryptoDetails()
    {
        ConsoleHelper.WriteSection("Crypto Details");

        return new CryptoPaymentDetails
        {
            WalletAddress = ConsoleHelper.ReadRequiredString("Wallet address"),
            Network = ConsoleHelper.ReadRequiredString("Network")
        };
    }
}
using PaymentProcessingSystem.Application.Services;
using PaymentProcessingSystem.Presentation.InputHandlers;
using PaymentProcessingSystem.Presentation.Output;

namespace PaymentProcessingSystem.Presentation.Menus;

public class MainMenu
{
    private readonly PaymentService _paymentService;
    private readonly RefundService _refundService;
    private readonly TransactionQueryService _transactionQueryService;
    private readonly PaymentInputHandler _paymentInputHandler;

    public MainMenu(
        PaymentService paymentService,
        RefundService refundService,
        TransactionQueryService transactionQueryService,
        PaymentInputHandler paymentInputHandler)
    {
        _paymentService = paymentService;
        _refundService = refundService;
        _transactionQueryService = transactionQueryService;
        _paymentInputHandler = paymentInputHandler;
    }

    public void Show()
    {
        var exit = false;

        while (!exit)
        {
            ConsoleHelper.WriteHeader("Payment Processing System");

            Console.WriteLine("1. Process Payment");
            Console.WriteLine("2. Refund Payment");
            Console.WriteLine("3. Check Payment Status");
            Console.WriteLine("4. View Transaction History");
            Console.WriteLine("5. Exit");
            Console.WriteLine();

            var choice = ConsoleHelper.ReadInt("Select an option");

            switch (choice)
            {
                case 1:
                    ProcessPayment();
                    break;
                case 2:
                    RefundPayment();
                    break;
                case 3:
                    CheckPaymentStatus();
                    break;
                case 4:
                    ViewTransactionHistory();
                    break;
                case 5:
                    exit = true;
                    break;
                default:
                    ConsoleHelper.WriteWarning("Invalid choice.");
                    ConsoleHelper.Pause();
                    break;
            }
        }
    }

    private void ProcessPayment()
    {
        ConsoleHelper.WriteHeader("Process Payment");

        var request = _paymentInputHandler.ReadPaymentRequest();
        var result = _paymentService.ProcessPayment(request);

        Console.WriteLine();

        if (result.IsSuccess && result.Data is not null)
        {
            ConsoleHelper.WriteSuccess(result.Message);
            Console.WriteLine($"Transaction ID : {result.Data.Id}");
            Console.WriteLine($"Amount         : {result.Data.Amount}");
            Console.WriteLine($"Fee            : {result.Data.Fee}");
            Console.WriteLine($"Net Amount     : {result.Data.NetAmount}");
            Console.WriteLine($"Currency       : {result.Data.Currency}");
            Console.WriteLine($"Method         : {result.Data.PaymentMethodType}");
            Console.WriteLine($"Status         : {result.Data.Status}");
            Console.WriteLine($"Created At     : {result.Data.CreatedAt}");
        }
        else
        {
            ConsoleHelper.WriteError(result.Message);
        }

        ConsoleHelper.Pause();
    }

    private void RefundPayment()
    {
        ConsoleHelper.WriteHeader("Refund Payment");

        var transactionId = ConsoleHelper.ReadRequiredString("Enter transaction ID");
        var result = _refundService.Refund(transactionId);

        Console.WriteLine();

        if (result.IsSuccess)
            ConsoleHelper.WriteSuccess(result.Message);
        else
            ConsoleHelper.WriteError(result.Message);

        ConsoleHelper.Pause();
    }

    private void CheckPaymentStatus()
    {
        ConsoleHelper.WriteHeader("Check Payment Status");

        var transactionId = ConsoleHelper.ReadRequiredString("Enter transaction ID");
        var transaction = _transactionQueryService.GetById(transactionId);

        Console.WriteLine();

        if (transaction is null)
        {
            ConsoleHelper.WriteError("Transaction not found.");
            ConsoleHelper.Pause();
            return;
        }

        ConsoleHelper.WriteSuccess("Transaction found.");
        Console.WriteLine($"Transaction ID : {transaction.Id}");
        Console.WriteLine($"Amount         : {transaction.Amount}");
        Console.WriteLine($"Fee            : {transaction.Fee}");
        Console.WriteLine($"Net Amount     : {transaction.NetAmount}");
        Console.WriteLine($"Currency       : {transaction.Currency}");
        Console.WriteLine($"Method         : {transaction.PaymentMethodType}");
        Console.WriteLine($"Status         : {transaction.Status}");
        Console.WriteLine($"Description    : {transaction.Description}");
        Console.WriteLine($"Customer Ref   : {transaction.CustomerReference}");
        Console.WriteLine($"Fraud Flagged  : {transaction.FraudFlagged}");
        Console.WriteLine($"Created At     : {transaction.CreatedAt}");
        Console.WriteLine($"Processed At   : {transaction.ProcessedAt}");
        Console.WriteLine($"Refunded       : {transaction.IsRefunded}");

        if (!string.IsNullOrWhiteSpace(transaction.FailureReason))
            Console.WriteLine($"Failure Reason : {transaction.FailureReason}");

        if (transaction.RefundedAt.HasValue)
            Console.WriteLine($"Refunded At    : {transaction.RefundedAt}");

        ConsoleHelper.Pause();
    }

    private void ViewTransactionHistory()
    {
        ConsoleHelper.WriteHeader("Transaction History");

        var transactions = _transactionQueryService.GetAll().ToList();

        if (!transactions.Any())
        {
            ConsoleHelper.WriteWarning("No transactions found.");
            ConsoleHelper.Pause();
            return;
        }

        foreach (var transaction in transactions)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('-', 60));
            Console.ResetColor();

            Console.WriteLine($"Transaction ID : {transaction.Id}");
            Console.WriteLine($"Amount         : {transaction.Amount}");
            Console.WriteLine($"Fee            : {transaction.Fee}");
            Console.WriteLine($"Net Amount     : {transaction.NetAmount}");
            Console.WriteLine($"Currency       : {transaction.Currency}");
            Console.WriteLine($"Method         : {transaction.PaymentMethodType}");
            Console.WriteLine($"Status         : {transaction.Status}");
            Console.WriteLine($"Created At     : {transaction.CreatedAt}");
            Console.WriteLine($"Refunded       : {transaction.IsRefunded}");

            if (!string.IsNullOrWhiteSpace(transaction.FailureReason))
                Console.WriteLine($"Failure Reason : {transaction.FailureReason}");
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(new string('-', 60));
        Console.ResetColor();

        ConsoleHelper.Pause();
    }
}
using Microsoft.Extensions.DependencyInjection;
using PaymentProcessingSystem.Application.Factories;
using PaymentProcessingSystem.Application.Services;
using PaymentProcessingSystem.Domain.Interfaces;
using PaymentProcessingSystem.Infrastructure.FeeStrategies;
using PaymentProcessingSystem.Infrastructure.Fraud;
using PaymentProcessingSystem.Infrastructure.Generators;
using PaymentProcessingSystem.Infrastructure.Logging;
using PaymentProcessingSystem.Infrastructure.Processors;
using PaymentProcessingSystem.Infrastructure.Repositories;
using PaymentProcessingSystem.Presentation.InputHandlers;
using PaymentProcessingSystem.Presentation.Menus;
namespace PaymentProcessingSystem.DependencyInjection;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Logging
        services.AddSingleton<IAppLogger, FileAppLogger>();
        // Repositories
        services.AddSingleton<IPaymentRepository, InMemoryPaymentRepository>();


        // Generators
        services.AddSingleton<ITransactionIdGenerator, TransactionIdGenerator>();

        // Fraud
        services.AddSingleton<IFraudChecker, MockFraudChecker>();

        // Fee Strategies
        //these it will create a list of fee strategies and the resolver will select the appropriate one based on the payment method type
        services.AddSingleton<IFeeStrategy, CreditCardFeeStrategy>();
        services.AddSingleton<IFeeStrategy, PayPalFeeStrategy>();
        services.AddSingleton<IFeeStrategy, BankTransferFeeStrategy>();
        services.AddSingleton<IFeeStrategy, CryptoFeeStrategy>();

        services.AddSingleton<IFeeStrategyResolver, FeeStrategyResolver>();

        // Payment Processors
        //these it will create a list of payment processors and the factory will select the appropriate one based on the payment method type
        services.AddSingleton<IPaymentProcessor, CreditCardPaymentProcessor>();
        services.AddSingleton<IPaymentProcessor, PayPalPaymentProcessor>();
        services.AddSingleton<IPaymentProcessor, BankTransferPaymentProcessor>();
        services.AddSingleton<IPaymentProcessor, CryptoPaymentProcessor>();

        // Factory
        services.AddSingleton<IPaymentProcessorFactory, PaymentProcessorFactory>();

        // Services
        services.AddSingleton<PaymentService>();
        services.AddSingleton<RefundService>();
        services.AddSingleton<TransactionQueryService>();

        // Presentation
        services.AddSingleton<PaymentInputHandler>();
        services.AddSingleton<MainMenu>();

        return services;
    }
}
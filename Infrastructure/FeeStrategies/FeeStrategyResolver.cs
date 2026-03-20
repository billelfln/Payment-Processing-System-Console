using PaymentProcessingSystem.Domain.Enums;
using PaymentProcessingSystem.Domain.Interfaces;
namespace PaymentProcessingSystem.Infrastructure.FeeStrategies;
public class FeeStrategyResolver : IFeeStrategyResolver
{

    private readonly IEnumerable<IFeeStrategy> _strategies;

    public FeeStrategyResolver(IEnumerable<IFeeStrategy> strategies)
    {
        _strategies = strategies;
    }

    public IFeeStrategy Resolve(PaymentMethodType methodType)
    {
        return _strategies.First(s => s.MethodType == methodType);
    }


}
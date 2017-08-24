namespace EthereumSamurai.EventProvider.Api.Mediator
{
    public sealed class ApiMediatorBuilder : IApiMediatorBuilder
    {
        public IApiMediator Build()
        {
            return new ApiMediator();
        }
    }
}
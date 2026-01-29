namespace Localizator.Shared.Mediator.Interfaces;

public interface IPipelineBehavior<in TRequest, TResponse> where TRequest : IRequest
{
    Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken);
}

public delegate Task<TResponse> RequestHandlerDelegate<TResponse>();

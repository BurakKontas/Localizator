using Localizator.Shared.Mediator.Interfaces;

namespace Localizator.Shared.Base;

public abstract class BaseDomainEvent(Guid id) : IRequest
{
    public Guid Id { get; } = id;
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    protected BaseDomainEvent() : this(Guid.NewGuid()) { }
}

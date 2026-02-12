using Localizator.Shared.Base;

namespace Localizator.Namespace.Domain.Namespace.Events;

public sealed class NamespaceMadePrivateDomainEvent(long namespaceId) : BaseDomainEvent
{
    public long NamespaceId { get; } = namespaceId;
}

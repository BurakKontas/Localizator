using Localizator.Shared.Base;

namespace Localizator.Namespace.Domain.Namespace.Events;

public sealed class NamespaceMadePublicDomainEvent(long namespaceId) : BaseDomainEvent
{
    public long NamespaceId { get; } = namespaceId;
}

using Localizator.Shared.Base;

namespace Localizator.Namespace.Domain.Namespace.Events;

public sealed class NamespaceUnpublishedDomainEvent(long namespaceId) : BaseDomainEvent
{
    public long NamespaceId { get; } = namespaceId;
}

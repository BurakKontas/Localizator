using Localizator.Shared.Base;

namespace Localizator.Namespace.Domain.Namespace.Events;

public sealed class NamespaceArchivedDomainEvent(long namespaceId) : BaseDomainEvent
{
    public long NamespaceId { get; } = namespaceId;
}

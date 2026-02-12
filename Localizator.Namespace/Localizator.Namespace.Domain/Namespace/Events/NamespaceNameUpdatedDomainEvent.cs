using Localizator.Shared.Base;

namespace Localizator.Namespace.Domain.Namespace.Events;

public sealed class NamespaceNameUpdatedDomainEvent(long namespaceId, string newName) : BaseDomainEvent
{
    public long NamespaceId { get; } = namespaceId;
    public string NewName { get; } = newName;
}

using Localizator.Shared.Base;

namespace Localizator.Namespace.Domain.Namespace.Events;

public sealed class NamespaceCreatedDomainEvent(long namespaceId, string name, string slug) : BaseDomainEvent
{
    public long NamespaceId { get; } = namespaceId;
    public string Name { get; } = name;
    public string Slug { get; } = slug;
}

using Localizator.Shared.Base;

namespace Localizator.Namespace.Domain.Namespace.Events;

public sealed class NamespacePublishedDomainEvent(long namespaceId, string publishedBy, DateTime publishedAt) : BaseDomainEvent
{
    public long NamespaceId { get; } = namespaceId;
    public string PublishedBy { get; } = publishedBy;
    public DateTime PublishedAt { get; } = publishedAt;
}

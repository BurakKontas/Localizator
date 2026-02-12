using Localizator.Shared.Base;

namespace Localizator.Namespace.Domain.Namespace.Events;

public sealed class LanguageAddedDomainEvent(long namespaceId, string languageCode) : BaseDomainEvent
{
    public long NamespaceId { get; } = namespaceId;
    public string LanguageCode { get; } = languageCode;
}

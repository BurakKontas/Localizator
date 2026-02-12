using Localizator.Namespace.Domain.Namespace.Enums;
using Localizator.Shared.Base;

namespace Localizator.Namespace.Domain.Namespace.Events;

public sealed class NamespaceVersionBumpedDomainEvent(long namespaceId, string newVersion, VersionBumpType bumpType) : BaseDomainEvent
{
    public long NamespaceId { get; } = namespaceId;
    public string NewVersion { get; } = newVersion;
    public VersionBumpType BumpType { get; } = bumpType;
}
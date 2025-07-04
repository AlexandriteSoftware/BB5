using System;

namespace BB5;

public class TrimTextAttribute
    : Attribute;

public class MultilineTextAttribute
    : Attribute;

/// <summary>
///     Specifies where to look for form-related attributes
///     for the current member or type.
/// </summary>
[AttributeUsage(
    AttributeTargets.Method
    | AttributeTargets.Property
    | AttributeTargets.Class,
    AllowMultiple = true)]
public class GetAnnotationsFromAttribute(
        Type sourceType,
        string? memberName = null)
    : Attribute
{
    public Type SourceType { get; } = sourceType;
    public string? MemberName { get; } = memberName;
}

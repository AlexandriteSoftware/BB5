using System;
using BB5.Models;

namespace BB5;

[AttributeUsage(AttributeTargets.Property)]
public class TextAreaAttribute
    : Attribute;

[AttributeUsage(
    AttributeTargets.Class
    | AttributeTargets.Property)]
public class ComponentSizeAttribute(
        ComponentSize size)
    : Attribute
{
    public ComponentSize Size => size;
}

[AttributeUsage(AttributeTargets.Property)]
public class InputTextPlaceholderAttribute(
        string text)
    : Attribute
{
    public string Text => text;
}

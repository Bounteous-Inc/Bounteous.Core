using System;

namespace Bounteous.Core.Utilities;

[AttributeUsage(AttributeTargets.Property)]
public class IgnoreForReflectionEqualsAttribute : Attribute
{
    public IgnoreForReflectionEqualsAttribute(string reason = "")
    {
        Reason = reason;
    }

    public string Reason { get; set; }
}
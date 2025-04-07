using System;

namespace Bounteous.Core;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class IgnoreIocRegistrationAttribute(string reason) : Attribute
{
    public string Reason { get; } = reason;
}
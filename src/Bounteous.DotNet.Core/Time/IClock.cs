using System;

namespace Bounteous.DotNet.Core.Time;

public interface IClock
{
    DateTime Now { get; }
    DateTime NowUtc { get; }
    DateTime Today { get; }
    void Freeze();
    void Freeze(DateTime timeToFreeze);
    void Thaw();
}
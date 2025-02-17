using System;
using System.Threading;
using Bounteous.DotNet.Core.Extensions;
using Bounteous.DotNet.Core.Time;
using FluentAssertions;
using Xunit;

namespace Bounteous.DotNet.Core.Test.Core.Time
{
    [Collection("base")]
    public class FreezeClockTest : IDisposable
    {
        public FreezeClockTest()
        {
            Clock.Local.Thaw();
            Clock.Utc.Thaw();
            UtcClock.Thaw();
        }

        public void Dispose()
        {
            Clock.Local.Thaw();
            Clock.Utc.Thaw();
            UtcClock.Thaw();
        }

        [Fact]
        public void CanFreezeClockUtc()
        {
            DateTime then;
            using (new FreezeClock())
            {
                then = Clock.Utc.Now;
                Thread.Sleep(500);
                Clock.Utc.Now.IsCloseEnough(then, .0001m);
            }

            Thread.Sleep(500);
            Clock.Utc.Now.Should().NotBe(then);
        }

        [Fact]
        public void CanFreezeClock()
        {
            DateTime then;
            using (new FreezeClock())
            {
                then = Clock.Local.Now;
                Thread.Sleep(500);
                Clock.Local.Now.IsCloseEnough(then, .0001m);
            }

            Thread.Sleep(500);
            Clock.Local.Now.Should().NotBe(then);
        }

        [Fact]
        public void CanFreezeClockForSpecificDateTime()
        {
            var then = new DateTime(2018, 01, 15);
            using (new FreezeClock(then))
            {
                Clock.Local.Now.IsCloseEnough(then, .0001m);
                Thread.Sleep(500);
                Clock.Local.Now.IsCloseEnough(then, .0001m);
            }

            Thread.Sleep(500);
            Clock.Local.Now.IsCloseEnough(then, .0001m);
        }

        [Fact]
        public void CanFreezeClockForSpecificDateTimeUtc()
        {
            var then = new DateTime(2018, 01, 15);
            using (new FreezeClock(then))
            {
                Clock.Utc.Now.IsCloseEnough(then, .0001m);
                Thread.Sleep(500);
                Clock.Utc.Now.IsCloseEnough(then, .0001m);
            }

            Thread.Sleep(500);
            Clock.Utc.Now.IsCloseEnough(then, .0001m);
        }
    }
}
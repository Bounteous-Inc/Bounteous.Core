using System;
using System.Threading.Tasks;
using Bounteous.Core.Utilities;
using FluentAssertions;
using Xunit;

namespace Bounteous.Core.Test.Utilities
{
    public class PerformanceTracerTests
    {
        [Fact]
        public void TracePerformance_Action_ExecutesSuccessfully()
        {
            var executed = false;

            new Action(() => { executed = true; }).TracePerformance("Test Action");

            executed.Should().BeTrue();
        }

        [Fact]
        public void TracePerformance_Func_ReturnsExpectedResult()
        {
            const int expected = 42;

            var result = new Func<int>(() => expected).TracePerformance("Test Func");

            result.Should().Be(expected);
        }

        [Fact]
        public async Task TracePerformanceAsync_Task_ExecutesSuccessfully()
        {
            var executed = false;

            await new Func<Task>(async () =>
            {
                await Task.Delay(10);
                executed = true;
            }).TracePerformanceAsync("Test Async Task");

            executed.Should().BeTrue();
        }

        [Fact]
        public async Task TracePerformanceAsync_TaskOfT_ReturnsExpectedResult()
        {
            const int expected = 99;

            var result = await new Func<Task<int>>(async () =>
            {
                await Task.Delay(10);
                return expected;
            }).TracePerformanceAsync("Test Async Func");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TracePerformance_Action_ThrowsOnNull()
            =>
                Assert.Throws<ArgumentNullException>(() => ((Action)null).TracePerformance("Null Action"));

        [Fact]
        public void TracePerformance_Func_ThrowsOnNull()
            => Assert.Throws<ArgumentNullException>(() => ((Func<int>)null).TracePerformance("Null Func"));

        [Fact]
        public async Task TracePerformanceAsync_Task_ThrowsOnNull()
            => await Assert.ThrowsAsync<ArgumentNullException>(()
                => ((Func<Task>)null).TracePerformanceAsync("Null Async Task"));


        [Fact]
        public async Task TracePerformanceAsync_TaskOfT_ThrowsOnNull()
            => await Assert.ThrowsAsync<ArgumentNullException>(()
                => ((Func<Task<int>>)null).TracePerformanceAsync("Null Async Func"));
    }
}
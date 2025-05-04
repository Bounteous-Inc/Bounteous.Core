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
            this.PerformanceTrace(() => { executed = true; }, "Test Action");
            executed.Should().BeTrue();
        }

        [Fact]
        public void TracePerformance_Func_ReturnsExpectedResult()
        {
            const int expected = 42;
            var result = this.PerformanceTrace(() => expected, "Tes Func");
            result.Should().Be(expected);
        }

        [Fact]
        public async Task TracePerformanceAsync_Task_ExecutesSuccessfully()
        {
            var executed = false;

            await this.PerformanceTraceAsync(async () =>
            {
                await Task.Delay(10);
                executed = true;
            }, "Test Task");
            
            executed.Should().BeTrue();
        }

        [Fact]
        public async Task TracePerformanceAsync_TaskOfT_ReturnsExpectedResult()
        {
            const int expected = 99;

            var result = await this.PerformanceTraceAsync(async () =>
                {
                    await Task.Delay(10);
                   return expected;
                }, "Test Task");
            
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TracePerformance_Action_ThrowsOnNull()
            => Assert.Throws<ArgumentNullException>(() 
                => this.PerformanceTrace(null, "Null Action"));

        [Fact]
        public void TracePerformance_Func_ThrowsOnNull()
            => Assert.Throws<ArgumentNullException>(() 
                => this.PerformanceTrace<int>(null, "Null Func"));

        [Fact]
        public async Task TracePerformanceAsync_Task_ThrowsOnNull()
            => await Assert.ThrowsAsync<ArgumentNullException>(()
                => this.PerformanceTraceAsync(null, "Null Async Task"));


        [Fact]
        public async Task TracePerformanceAsync_TaskOfT_ThrowsOnNull()
            => await Assert.ThrowsAsync<ArgumentNullException>(()
                => this.PerformanceTraceAsync<int>(null, "Null Async Func"));
    }
}
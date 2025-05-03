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
            this.TracePerformance(() => { executed = true; }, "Test Action");
            executed.Should().BeTrue();
        }

        [Fact]
        public void TracePerformance_Func_ReturnsExpectedResult()
        {
            const int expected = 42;
            var result = this.TracePerformance(() => expected, "Tes Func");
            result.Should().Be(expected);
        }

        [Fact]
        public async Task TracePerformanceAsync_Task_ExecutesSuccessfully()
        {
            var executed = false;

            await this.TracePerformanceAsync(async () =>
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

            var result = await this.TracePerformanceAsync(async () =>
                {
                    await Task.Delay(10);
                   return expected;
                }, "Test Task");
            
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TracePerformance_Action_ThrowsOnNull()
            => Assert.Throws<ArgumentNullException>(() 
                => this.TracePerformance(null, "Null Action"));

        [Fact]
        public void TracePerformance_Func_ThrowsOnNull()
            => Assert.Throws<ArgumentNullException>(() 
                => this.TracePerformance<int>(null, "Null Func"));

        [Fact]
        public async Task TracePerformanceAsync_Task_ThrowsOnNull()
            => await Assert.ThrowsAsync<ArgumentNullException>(()
                => this.TracePerformanceAsync(null, "Null Async Task"));


        [Fact]
        public async Task TracePerformanceAsync_TaskOfT_ThrowsOnNull()
            => await Assert.ThrowsAsync<ArgumentNullException>(()
                => this.TracePerformanceAsync<int>(null, "Null Async Func"));
    }
}
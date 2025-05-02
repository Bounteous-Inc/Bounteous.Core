using Bounteous.Core.Extensions;

namespace Bounteous.Core.Utilities;

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Serilog;

public static class PerformanceTracer
{
    private const string UnnamedTask = "Unnamed Task";
    
    public static void TracePerformance(this Action action, string taskName)
    {
        ArgumentNullException.ThrowIfNull(action);
        taskName = taskName.UseDefault("UnnamedTask");

        Log.Information("Starting task: {TaskName}", taskName);
        
        var stopwatch = Stopwatch.StartNew();
        action();
        stopwatch.Stop();
 
        Log.Information("Completed task: {TaskName} in {ElapsedMilliseconds} ms", taskName,
            stopwatch.ElapsedMilliseconds);
    }

    public static T TracePerformance<T>(this Func<T> func, string taskName)
    {
        ArgumentNullException.ThrowIfNull(func);
        taskName = taskName.UseDefault("UnnamedTask");

        Log.Information("Starting task: {TaskName}", taskName);
        
        var stopwatch = Stopwatch.StartNew();
        var result = func();
        stopwatch.Stop();

        Log.Information("Completed task: {TaskName} in {ElapsedMilliseconds} ms", taskName,
            stopwatch.ElapsedMilliseconds);

        return result;
    }

    public static async Task TracePerformanceAsync(this Func<Task> taskFunc, string taskName)
    {
        ArgumentNullException.ThrowIfNull(taskFunc);
        taskName = taskName.UseDefault("UnnamedTask");

        Log.Information("Starting async task: {TaskName}", taskName);

        var stopwatch = Stopwatch.StartNew();
        await taskFunc();
        stopwatch.Stop();
        
        Log.Information("Completed async task: {TaskName} in {ElapsedMilliseconds} ms", taskName,
            stopwatch.ElapsedMilliseconds);
    }

    public static async Task<T> TracePerformanceAsync<T>(this Func<Task<T>> taskFunc, string taskName)
    {
        ArgumentNullException.ThrowIfNull(taskFunc);
        taskName = taskName.UseDefault("UnnamedTask");

        Log.Information("Starting async task: {TaskName}", taskName);
        
        var stopwatch = Stopwatch.StartNew();
        var result = await taskFunc();
        stopwatch.Stop();

        Log.Information("Completed async task: {TaskName} in {ElapsedMilliseconds} ms", taskName,
            stopwatch.ElapsedMilliseconds);

        return result;
    }
}

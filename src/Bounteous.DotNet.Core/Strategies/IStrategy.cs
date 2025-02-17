using System.Threading.Tasks;

namespace Bounteous.DotNet.Core.Strategies;

public interface IStrategy<T>
{
    Task<T> RunAsync(T subject);
}
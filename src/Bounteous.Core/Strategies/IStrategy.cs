using System.Threading.Tasks;

namespace Bounteous.Core.Strategies;

public interface IStrategy<T>
{
    Task<T> RunAsync(T subject);
}
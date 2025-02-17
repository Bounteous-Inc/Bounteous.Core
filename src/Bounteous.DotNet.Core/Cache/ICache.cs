using System;
using System.Threading.Tasks;

namespace Bounteous.DotNet.Core.Cache;

public interface ICache
{
    Task<TItem> GetOrCreate<TItem>(object key, Func<Task<TItem>> createItem);
}
using System.Threading.Tasks;

namespace Bounteous.Core.Commands;

public interface ICommand
{
    void Run();
}

public interface IWaitedCommand
{
    Task RunAsync();
}

public interface ICommand<in TInput>
{
    Task RunAsync(TInput data);
}
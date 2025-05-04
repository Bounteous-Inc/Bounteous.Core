using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bounteous.Core.Commands;

public class CompositeWaitedCommand : IWaitedCommand
{
    private readonly ICollection<IWaitedCommand> commands = new List<IWaitedCommand>();
    
    public async Task RunAsync()
    {
        foreach (var command in commands)
            await command.RunAsync();
    }

    public void Add(IWaitedCommand waitedCommand)
        => commands.Add(waitedCommand);
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bounteous.DotNet.Core.Utilities.ApplicationEvents;

public interface IEventSink
{
    Task SendAsync(ApplicationEvent applicationEvent);
    Task SendAsync(IEnumerable<ApplicationEvent> applicationEvents);
}
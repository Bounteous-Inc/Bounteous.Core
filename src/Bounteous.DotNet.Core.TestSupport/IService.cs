using System;

namespace Bounteous.DotNet.Core.TestSupport;

public interface IService
{
    void DoIt();
}

public class MyService : IService
{
    public void DoIt()
    {
        Console.WriteLine("Hi");
    }
}
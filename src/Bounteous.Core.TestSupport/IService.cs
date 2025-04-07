namespace Bounteous.Core.TestSupport;

public interface IService {}
public class ServiceImplementation : IService { }
public class DefaultServiceImplementation : IService { }
public class OriginalServiceImplementation : IService { } 
public class NewServiceImplementation : IService { }

public interface IHaveNoConcreteClass {}


public interface IAutoRegisterAll {}
public class AutoRegister1 : IAutoRegisterAll {}
public class AutoRegister2 : IAutoRegisterAll {}
public class AutoRegister3 : IAutoRegisterAll {}

[IgnoreIocRegistration("should be ignored")]
public class AutoRegisterIgnored : IAutoRegisterAll {}
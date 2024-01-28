namespace CoffeeHouse.Core.Injectable
{
    public interface IDependency
    {
    }

    public interface ITransientDependency: IDependency
    {
    }

    public interface IScopedDependency : IDependency
    {
    }

    public interface ISingletonDependency : IDependency
    {
    }

    public interface IKeyedDependency : IDependency
    {
    }
}

using Mopups.Services;
using CoffeeHouse.Core.Helpers;
using CoffeeHouse.Core.Injectable;
using Scrutor;

namespace CoffeeHouse.App.Extensions
{
    public static partial class ConfigExtensions
    {
        public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            // Add your services here...
            builder.Services.AddSingleton(Connectivity.Current);
            builder.Services.AddSingleton(MopupService.Instance);

            builder.Services.Scan(scan => scan
            // We start out with all types in the assembly of IDependency            
            .FromAssemblies(AssemblyHelper.GetAllAssemblies(SearchOption.AllDirectories))
                // AddClasses starts out with all public, non-abstract types in this assembly.
                // These types are then filtered by the delegate passed to the method.
                // In this case, we filter out only the classes that are assignable to ITransientService.
                .AddClasses(classes => classes.AssignableTo<ITransientDependency>())
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    // We then specify what type we want to register these classes as.
                    // In this case, we want to register the types as all of its implemented interfaces.
                    // So if a type implements 3 interfaces; A, B, C, we'd end up with three separate registrations.
                    .AsSelfWithInterfaces()
                    // And lastly, we specify the lifetime of these registrations.
                    .WithTransientLifetime()
                // Here we start again, with a new full set of classes from the assembly above.
                // This time, filtering out only the classes assignable to IScopedService.
                .AddClasses(classes => classes.AssignableTo<IScopedDependency>())
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    // Now, we just want to register these types as a single interface, IScopedDependency.
                    .AsSelfWithInterfaces()
                    // And again, just specify the lifetime.
                    .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo<ISingletonDependency>())
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    // Now, we just want to register these types as a single interface, ISingletonDependency.
                    .AsSelfWithInterfaces()
                    // And again, just specify the lifetime.
                    .WithSingletonLifetime());

            return builder;
        }
    }
}

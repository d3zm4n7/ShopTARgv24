using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopTARgv24.ApplicationServices.Services;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;
using ShopTARgv24.SpaceshipTest.Macros;
using ShopTARgv24.SpaceshipTest.Mock;


namespace ShopTARgv24.SpaceshipTest
{
    public abstract class TestBase
    {
        protected IServiceProvider Services { get; set; }

        protected TestBase()
        {
            var sc = new ServiceCollection();
            SetupServices(sc);

            Services = sc.BuildServiceProvider();
        }

        public virtual void SetupServices(IServiceCollection services)
        {
            services.AddScoped<ISpaceshipsServices, SpaceshipsServices>();
            services.AddScoped<IFileServices, FileServices>();
            services.AddScoped<IHostEnvironment, MockHostEnvironment>();

            services.AddDbContext<ShopTARgv24Context>(cfg =>
            {
                cfg.UseInMemoryDatabase("SpaceshipTestDb");
                cfg.ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            RegisterMacros(services);
        }

        private void RegisterMacros(IServiceCollection services)
        {
            var macrosBase = typeof(IMacros);

            var macroTypes = macrosBase.Assembly.GetTypes()
                .Where(t => macrosBase.IsAssignableFrom(t) &&
                       !t.IsAbstract &&
                       !t.IsInterface);
        }

        protected T Svc<T>() => Services.GetRequiredService<T>();
    }
}

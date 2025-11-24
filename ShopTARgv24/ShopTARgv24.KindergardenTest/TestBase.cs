using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopTARgv24.ApplicationServices.Services;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;
using ShopTARgv24.KindergardenTest.Macros;
using ShopTARgv24.KindergardenTest.Mock;

namespace ShopTARgv24.KindergardenTest
{
    public abstract class TestBase
    {
        protected IServiceProvider serviceProvider { get; set; }

        protected TestBase()
        {
            var services = new ServiceCollection();
            SetupServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        public virtual void SetupServices(IServiceCollection services)
        {
            // Важно: используем правильные имена классов с 's' в середине
            services.AddScoped<IKindergardensServices, KindergardensServices>();
            services.AddScoped<IFileServices, FileServices>();
            services.AddScoped<IHostEnvironment, MockHostEnvironment>();

            services.AddDbContext<ShopTARgv24Context>(cfg =>
            {
                cfg.UseInMemoryDatabase("KindergardenTestDb");
                cfg.ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            RegisterMacros(services);
        }

        private void RegisterMacros(IServiceCollection services)
        {
            var macroBaseType = typeof(IMacros);
            var macros = macroBaseType.Assembly.GetTypes()
                .Where(t => macroBaseType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var macro in macros)
            {
                services.AddTransient(macro);
            }
        }

        protected T Svc<T>()
        {
            return serviceProvider.GetService<T>();
        }
    }
}
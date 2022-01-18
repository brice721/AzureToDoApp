using Autofac;
using AzureFunctions.Autofac.Configuration;
using AzureFunctions.Autofac.Shared.Extensions;
using Microsoft.Extensions.Logging;

namespace AzureFunction.Autofac
{
    public class DependencyInjectionInitializer
    {
        public DependencyInjectionInitializer(string functionName, ILoggerFactory loggerFactory)
        {
            DependencyInjection.Initialize(builder =>
            {
                builder.RegisterModule(new ToDoApplicationModule());
                builder.RegisterLoggerFactory(loggerFactory);
            }, functionName);
        }
    }
}

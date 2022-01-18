using Domain;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AzureFunction.Startup))]
namespace AzureFunction
{
    public class Startup : FunctionsStartup
    {
        public AppSettings Settings { get; set; } = new AppSettings();

        public override void Configure(IFunctionsHostBuilder builder)
        {
            
        }
    }
}

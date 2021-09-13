using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Statistics;
using System;
using System.Threading.Tasks;

namespace MsOrleansKubernetes
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var hostBuilder = new HostBuilder();
            hostBuilder.ConfigureLogging(logging => logging.AddConsole());
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices(services =>
                {
                    services.AddControllers();
                    services.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new OpenApiInfo { Title = "MsOrleansKubernetes", Version = "v1" });
                    });
                });

                webBuilder.Configure((ctx, app) =>
                {
                    var env = ctx.HostingEnvironment;
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MsOrleansKubernetes v1"));
                    app.UseHttpsRedirection();
                    app.UseRouting();
                    app.UseAuthorization();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });
            });

            
            hostBuilder.UseOrleans(b =>
            {
                b.Configure<ClusterOptions>(opts =>
                {
                    opts.ClusterId = "mycluster";//
                    opts.ServiceId = "myapp";
                });
                b.UseKubernetesHosting();
                b.UseLinuxEnvironmentStatistics();
                b.UseDashboard(opts =>
                {
                    opts.Username = Environment.GetEnvironmentVariable("ORLEANS_DASHBOARD_USERNAME");
                    opts.Password = Environment.GetEnvironmentVariable("ORLEANS_DASHBOARD_PASSWORD");
                    opts.Host = Environment.GetEnvironmentVariable("ORLEANS_DASHBOARD_HOST");
                    opts.Port = Int32.Parse(Environment.GetEnvironmentVariable("ORLEANS_DASHBOARD_PORT"));
                    opts.HostSelf = true;
                    opts.CounterUpdateIntervalMs = 1000;
                });
                b.UseAzureStorageClustering(opts => opts.ConnectionString = "DefaultEndpointsProtocol=https;AccountName=mborleanstest;AccountKey=u0JZ/Agvagu9LsLu6dTraNWi7smrDwIEfAFsCnDSA9v7yojRvNbxFitDnqs7aTlkise2tqMimZrTtY+c29PVLw==;BlobEndpoint=https://mborleanstest.blob.core.windows.net/;QueueEndpoint=https://mborleanstest.queue.core.windows.net/;TableEndpoint=https://mborleanstest.table.core.windows.net/;FileEndpoint=https://mborleanstest.file.core.windows.net/;");
                b.ConfigureEndpoints(11111, 30000);
                b.ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(Program).Assembly).WithReferences());
            });

            await hostBuilder.RunConsoleAsync();
        }
    }
}

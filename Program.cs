using Autofac;
using EmailProject.Modules;
using EmailProject;
using EmailProject;
using EmailProject.Modules;
using System.Reflection;
using Autofac.Extensions.DependencyInjection;

namespace EmailProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();

            try
            {
                var hostBuilder = CreateHostBuilder(configuration, args);

                var configuredHost = hostBuilder.Build();

                configuredHost.Run();
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {

            }
        }

        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables().Build();
        }

        public static IHostBuilder CreateHostBuilder(IConfiguration configuration, string[] args) =>
            Host.CreateDefaultBuilder(args)
             .ConfigureContainer<ContainerBuilder>(builder =>
             {
                 builder.RegisterModule(new AutofacModule());
             })
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder.UseConfiguration(configuration);
                 webBuilder.UseStartup<Startup>();
             }).UseServiceProviderFactory(new AutofacServiceProviderFactory());
            
            }
}




//var builder = WebApplication.CreateBuilder(args);
//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

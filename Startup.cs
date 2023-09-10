using EmailProject.DataLayer.Context;
using EmailProject.Services;
using EmailProject.Services.Interface;
using EmailProject.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace EmailProject
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("UserConnectionString")));

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            Global.Initilazer();

        }

        [Obsolete]
        public void Configure(IApplicationBuilder app)
        {
            using (var service = app.ApplicationServices.GetRequiredService<DataContext>())
            {
                service.Database.Migrate();
            }

            string[] corsDomains = Configuration["CorsDomains:Domains"].Split(",");

            app.UseCors(options => options.WithOrigins(corsDomains).AllowAnyMethod().AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

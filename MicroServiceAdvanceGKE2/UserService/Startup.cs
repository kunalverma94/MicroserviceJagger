using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UserService
{
    public class Startup
    {
        public static string configVersion = "";
        public static string connectionstring = "";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var port = this.Configuration.GetValue<string>("port") != null ? this.Configuration.GetValue<string>("port") : "3306";
            configVersion = this.Configuration.GetValue<string>("ConfigVersion");
            connectionstring = @$"server={ this.Configuration.GetValue<string>("server")}
                                ;userid={ this.Configuration.GetValue<string>("userid")}
                                ;password={ this.Configuration.GetValue<string>("password")};
                                ;port= {port};
                                ;database=sys";
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

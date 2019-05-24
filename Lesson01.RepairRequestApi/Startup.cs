using Lesson01.RepairRequestApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lesson01.RepairRequestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services
                .AddHttpClient()
                .AddTransient<IUsersService, WebApiUsersService>()
                .AddTransient<IAllocationService, AllocationService>();
        }

        public void Configure(IApplicationBuilder app)
            => app.UseMvc()
                .UseMiddleware<ExceptionMiddleware>();
    }
}

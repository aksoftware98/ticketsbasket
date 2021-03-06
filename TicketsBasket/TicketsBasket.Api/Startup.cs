using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TicketsBasket.Models.Data;
using Microsoft.EntityFrameworkCore;
using TicketsBasket.Api.Extensions;
using TicketsBasket.Api.Middlewares;

namespace TicketsBasket.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddB2CAuthentication(Configuration);
            services.AddApplicationDatabaseContext(Configuration);
            services.AddUnitOfWork(); 
            services.AddBusinessServices(); 
            services.ConfigureCors();
            services.ConfigureIdentityOptions();
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddAzureStorageOptions(Configuration);
            services.AddInfrastructreServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseHttpsRedirection();

            app.UseMiddleware<ErrorHandlingMiddleware>(); 

            app.UseSwagger();
            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "TicketsBasket API V1.0");
            });

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            // Add the role claim
            app.UseMiddleware<CustomIdentityMiddleware>(); 
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

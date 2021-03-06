using AutoMapper;
using MakeSimple.Logging;
using MakeSimple.SharedKernel.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Org.VSATemplate.Infrastructure.Database;
using Org.VSATemplate.WebApi.Configs;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Org.VSATemplate.WebApi
{
    public class Startup
    {
        private const string applicationModule = ".Application";

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediaRModule(new MediaROptions()
            {
                OnValidatorPipeline = true,
                EndWithPattern = new List<string>() { applicationModule }
            });
            services.AddEfDbContext<CoreDBContext>();

            services.AddMakeSimpleLoging(new LoggingOption()
            {
                IsOffLogSystem = true,
                IsOutputJson = false,
                MinimumLevel = LoggerLevel.Information
            });

            services.AddSwagger();
            services.AddHealthChecks();
            services.AddAuthenticationExtension();
            services.AddApiVersioningExtension();
            services.AddAutoMapper(Assembly.GetEntryAssembly().GetReferencedAssemblies().Where(e => e.Name.EndsWith(applicationModule)).Select(e => Assembly.Load(e)));
            services.AddControllers().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionHandler("/Error");
            app.UseSwaggerDocs();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }
    }
}
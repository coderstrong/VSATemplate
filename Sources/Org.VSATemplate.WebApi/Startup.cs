using AutoMapper;
using MakeSimple.SharedKernel.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Org.VSATemplate.Application.Features.Students.Mappings;
using Org.VSATemplate.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Reflection;
using MakeSimple.Logging;
using Org.VSATemplate.WebApi.Configs;

namespace Org.VSATemplate.WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRestClientCore();
            services.AddMediaRModule(new MediaROptions()
            {
                OnValidatorPipeline = true,
                EndWithPattern = new List<string>() { ".Application" }
            });
            services.AddEfDbContext<CoreDBContext>();
            
            services.AddMakeSimpleLoging(new LoggingOption()
            {
                IsOffLogSystem = false,
                IsOutputJson = false,
                MinimumLevel = LoggerLevel.Information
            });

            services.AddSwagger();
            services.AddAuthenticationExtension();
            services.AddApiVersioningExtension();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerDocs();
            app.UseExceptionHandlerCore();
            app.UseRouting();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

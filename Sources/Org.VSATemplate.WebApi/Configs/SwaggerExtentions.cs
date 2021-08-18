using MakeSimple.SharedKernel.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Org.VSATemplate.WebApi.Configs
{
    public static class SwaggerExtentions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            var assembly = Assembly.GetEntryAssembly();

            services.AddSwaggerGen(s =>
            {
                s.AddServer(new OpenApiServer
                {
                    Url = "/"
                });
                s.SwaggerDoc("v1"
                    , new OpenApiInfo
                    {
                        Title = "ProjectName",
                        Version = $"v{assembly.GetName().Version}",
                        Description = $"Manage everything in your ProjectName" +
                        $"</br>Deployed at {assembly.GetCreationTime():dd/MM/yyyy HH:mm:ssK}",
                        Contact = new OpenApiContact
                        {
                            Name = "ProjectName Support",
                            Email = "support@ProjectName.com",
                            Url = new Uri("https://www.ProjectName.com/support"),
                        },
                    });
                s.AddSecurityDefinition("Bearer"
                    , new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please insert JWT with Bearer into field",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                s.DocumentFilter<JsonPatchDocumentFilter>();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "ProjectName v1");
                c.RoutePrefix = "swagger";
                c.DefaultModelsExpandDepth(2);
            });

            return app;
        }

        /// <summary>
        /// Generating a correct request schema with type JsonPatchDocument
        /// </summary>
        public class JsonPatchDocumentFilter : IDocumentFilter
        {
            public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
            {
                var schemas = swaggerDoc.Components.Schemas.ToList();
                foreach (var item in schemas)
                {
                    if (item.Key.StartsWith("Operation") || item.Key.StartsWith("JsonPatchDocument"))
                        swaggerDoc.Components.Schemas.Remove(item.Key);
                }

                swaggerDoc.Components.Schemas.Add("Operation", new OpenApiSchema
                {
                    Type = "object",
                    Properties = new Dictionary<string, OpenApiSchema>
                    {
                        {"op", new OpenApiSchema{ Type = "string" } },
                        {"value", new OpenApiSchema{ Type = "string"} },
                        {"path", new OpenApiSchema{ Type = "string" } }
                    }
                });

                swaggerDoc.Components.Schemas.Add("JsonPatchDocument", new OpenApiSchema
                {
                    Type = "array",
                    Items = new OpenApiSchema
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "Operation" }
                    },
                    Description = "Array of operations to perform"
                });

                foreach (var path in swaggerDoc.Paths.SelectMany(p => p.Value.Operations)
                .Where(p => p.Key == Microsoft.OpenApi.Models.OperationType.Patch))
                {
                    foreach (var item in path.Value.RequestBody.Content.Where(c => c.Key != "application/json"))
                        path.Value.RequestBody.Content.Remove(item.Key);
                    var response = path.Value.RequestBody.Content.Single(c => c.Key == "application/json");
                    response.Value.Schema = new OpenApiSchema
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "JsonPatchDocument" }
                    };
                }
            }
        }
    }
}
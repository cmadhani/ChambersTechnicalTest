using DocumentManagement.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DocumentManagementSolution
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
            services.AddControllers();

            services.AddSwaggerGen(setupAction =>
            {
                AddApiInfo(setupAction);
                AddXmlComments(setupAction);
            });

            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IModelStateErrorHandler, ModelStateErrorHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/DocumentManagementOpenApiSpecification/swagger.json", "Document Management Service");
            });
        }

        public class AssignContentTypeFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                if (operation.Responses.ContainsKey("200"))
                {
                    operation.Responses.Clear();
                }

                var data = new OpenApiResponse
                {
                    Description = "Ok",
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType(),
                    }
                };

                operation.Responses.Add("200", data);
            }
        }

        private static void AddApiInfo(SwaggerGenOptions options)
        {
            var apiInfo = new OpenApiInfo() { Title = "Document Management Service", Version = "1.0.0" };
            options.SwaggerDoc("DocumentManagementOpenApiSpecification", apiInfo);
            options.OperationFilter<AssignContentTypeFilter>();
        }

        private static void AddXmlComments(SwaggerGenOptions options)
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }

    }
}

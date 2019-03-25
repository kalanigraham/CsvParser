using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvParser.Helpers;
using CsvParser.Helpers.Interfaces;
using CsvParser.Providers;
using CsvParser.Providers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace CsvParser
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddApiVersioning(o => o.AssumeDefaultVersionWhenUnspecified = true);

            services.AddSwaggerGen(
                    options =>
                    {
                        var provider = services.BuildServiceProvider()
                                               .GetRequiredService<IApiVersionDescriptionProvider>();


                        foreach (var description in provider.ApiVersionDescriptions)
                        {
                            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                        }

                    });

            services.AddScoped<IParserProvider, ParserProvider>();
            services.AddScoped<ITokenizer, Tokenizer>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        static Info CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new Info()
            {
                Title = $"CsvParser API {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "CSV Parser for Henry Schein Test.",
                Contact = new Contact() { Name = "Kalani Graham", Email = "kalani.graham@gmail.com" },
                TermsOfService = "Free",
                License = new License() { Name = "MIT", Url = "https://opensource.org/licenses/MIT" }
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }

    }
}

using System;
using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Serialization;
using ApiSendEmail.Extensions;
using ApiSendEmail.FluentValidator;
using ApiSendEmail.Interfaces;
using ApiSendEmail.Models;
using ApiSendEmail.Settings;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace ApiSendEmail
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
            services.Configure<MailSettings>(x =>
            {
                x.Mail = new MailAddress(Configuration.GetValue<string>("MailSettings:Mail"));
                x.Host = Configuration.GetValue<string>("MailSettings:Host");
                x.Password = Configuration.GetValue<string>("MailSettings:Password");
                x.Port = Configuration.GetValue<int>("MailSettings:Port");
            });
            services.AddTransient<IMailService, Services.MailService>();

            services.AddControllers().AddNewtonsoftJson(opts =>
            {
                opts.SerializerSettings.Converters.Add(new MailAddressConverter());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiSendEmail", Version = "v1" });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiSendEmail v1"));
            }

            app.UseGlobalExceptionErrorHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

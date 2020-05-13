using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SimpleBankingSystem.Domain.Models.Entities;
using SimpleBankingSystem.Domain.Validators;
using System;
using System.IO;
using System.Reflection;

namespace SimpleBankingSystem.API
{
#pragma warning disable CS1591
    public class Startup
    {
        private const string AllowCustomOrigins = "AllowCustomOrigins";
        private const string ClientAppUrl = "https://localhost:44389";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<IAccountBalanceValidator, AccountBalanceValidator>();
            services.AddScoped<IAccountStatusValidator, AccountStatusValidator>();
            services.AddSingleton<IAccountEntity>(CreateAccountWithFixedGuid());
            services.AddSwaggerGen(generator =>
            {
                generator.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "SimpleBankingSystem API",
                    Description = "Web API for SimpleBankingSystem application",
                    Contact = new OpenApiContact
                    {
                        Name = "Marcin Wojaczek",
                        Email = "mwojaczek92@gmail.com"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                generator.IncludeXmlComments(xmlPath);
            });
            services.AddCors(options =>
            {
                options.AddPolicy(AllowCustomOrigins,
                builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowCredentials();
                    builder.AllowAnyMethod();
                    builder.WithOrigins(ClientAppUrl);
                });
            });
            services.AddMediatR(Assembly.Load("SimpleBankingSystem.Domain"));
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleBankingSystem API v1");
                options.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();
            app.UseCors(AllowCustomOrigins);
            app.UseMvc();
        }

        private static AccountEntity CreateAccountWithFixedGuid()
        {
            return new AccountEntity(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"));
        }
    }
#pragma warning restore CS1591
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerTestTask.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using CustomerTestTask.Data.IRepositories;
using CustomerTestTask.Data.Repositories;
using CustomerTestTask.Api.Providers;
using CustomerTestTask.Data.Entities;

namespace CustomerTestTask.Api
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin", builder => {
                    builder.AllowAnyOrigin();
                    builder.WithMethods("GET", "PUT", "POST", "DELETE");
                    builder.AllowAnyHeader();
                });
            });
        
            services.AddDbContext<Context>(options => options.UseInMemoryDatabase(databaseName: Configuration["DbName"]));
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddControllers(c => {
                c.ModelBinderProviders.Insert(0, new ListRequestViewModelBinderProvider<CustomerEntity>());
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v3", new OpenApiInfo { Title = "CustomerTestTask.Api", Version = "v3" });
                c.EnableAnnotations();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v3/swagger.json", "CustomerTestTask.Api v1"));
            }
            else 
            {
                app.UseHttpsRedirection();
            }

            

            app.UseRouting();

            app.UseCors("AllowAllOrigin");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

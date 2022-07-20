using AdminService.Helpers;
using AdminService.Models;
using AdminService.Repository;
using AdminService.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace AdminService
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
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Environment.GetEnvironmentVariable("redis") ?? Configuration.GetConnectionString("Redis");
                options.InstanceName = "SkillProfileAdmin_";
            });
            services.AddScoped<SkillProfileContext>();
            services.AddScoped<ISkillProfileRepository, SkillProfileRepository>();
            services.AddScoped<ISkillProfileService, SkillProfileService>();
            services.AddScoped<ICriteriaBuilder, CriteriaBuilder>();
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            var serviceConfiguration = Configuration.GetSection("RabbitMq");
            var serviceClientSetting = serviceConfiguration.Get<RabbitMqConfiguration>();
            services.Configure<RabbitMqConfiguration>(serviceConfiguration);

            services.AddSingleton<IServiceBusConsumer, ServiceBusConsumer>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod());
            });


            services.AddSwaggerGen(c =>
            c.SwaggerDoc("V1", new OpenApiInfo
            {
                Title = "SkillTracker - Admin services",
                Version = "V1",
                Description = "A simple API to search for skills of associates.",
                Contact = new OpenApiContact
                {
                    Name = "Hannah",
                    Email = "hannah.dp@cognizant.com"
                }
            }
            ));

            if(serviceClientSetting.Enabled)
            {
                services.AddHostedService<AddSkillProfileMessageReceiver>();
            }
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

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/V1/swagger.json", "Skill tracker admin API");
                c.RoutePrefix = string.Empty;
            });

            var bus = app.ApplicationServices.GetService<IServiceBusConsumer>();
            bus.ReceiveMessages().GetAwaiter().GetResult();
        }
    }
}

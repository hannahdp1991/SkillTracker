using EngineerService.Models;
using EngineerService.Repository;
using EngineerService.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace EngineerService
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
                options.InstanceName = "SkillProfileEngineer_";
            });
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddScoped<SkillProfileContext>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<ISkillService, SkillService>();

            var serviceConfiguration = Configuration.GetSection("RabbitMq");
            services.Configure<RabbitMqConfiguration>(serviceConfiguration);
            services.AddSingleton<IAddSkillProfileMessageSender, AddSkillProfileMessageSender>();

            services.AddScoped<ServiceBusSender>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod());
            });

            services.AddSwaggerGen(c =>
            c.SwaggerDoc("V1", new OpenApiInfo
            {
                Title = "SkillTracker - Engineer services",
                Version = "V1",
                Description = "A simple API to search for skills of associates.",
                Contact = new OpenApiContact
                {
                    Name = "Hannah",
                    Email = "hannah.dp@cognizant.com"
                }
            }
            ));
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
                c.SwaggerEndpoint("/swagger/V1/swagger.json", "Skill tracker engineer API");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}

using AsynchInnServ.Data;
using AsynchInnServ.Models;
using AsynchInnServ.Models.Interface;
using AsynchInnServ.Models.Interface.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //"Register a service"
            services.AddDbContext<AsynchInnDbContext>(options => {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });
            services.AddMvc();
            services.AddControllers();

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AsynchInnDbContext>();


            //here it is saying -- I need something of this type, from this.
            services.AddTransient<IHotel, HotelRepository>();
            services.AddTransient<IRoom, RoomRepository>();
            services.AddTransient<IAmmenities, AmmenityRepository>();
            services.AddTransient<IHotelRoom, HotelRoomRepository>();
            services.AddTransient<IUserService, IdentityUserService>();

            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
          );

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Matthews School",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "/api/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api/v1/swagger.json", "Matthews Hotels");
                options.RoutePrefix = "docs";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapControllers();
            });
        }
    }
}

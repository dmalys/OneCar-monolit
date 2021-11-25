using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OneCarProject.BusinessLayer.Car.Interfaces;
using OneCarProject.BusinessLayer.Car.Implementation;
using OneCarProject.DataAccessLayer.Repository;
using OneCarProject.DataAccessLayer.Interfaces;
using OneCarProject.DataAccessLayer.Repositories;
using OneCarProject.BusinessLayer.CarImage.Implementation;
using OneCarProject.BusinessLayer.CarImage.Interfaces;
using OneCarProject.BusinessLayer.Brand.Implementation;
using OneCarProject.BusinessLayer.User.Interfaces;
using OneCarProject.BusinessLayer.User.Implementation;
using OneCarProject.BusinessLayer.Ticket.Interfaces;
using OneCarProject.BusinessLayer.Ticket.Implementation;
using OneCarProject.Utility;
using OneCarProject.BusinessLayer.Login.Implementation;
using OneCarProject.BusinessLayer.Login.Interfaces;
using OneCarProject.BusinessLayer.Coupon.Implementation;
using OneCarProject.BusinessLayer.Coupon.Interfaces;

namespace OneCarProject
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
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My Awesome API",
                    Version = "v1"
                });
              //  c.OperationFilter<SwaggerFileOperationFilter>();

            });

            //register services
            services.AddTransient<ICarHandler, CarHandler>();
            services.AddTransient<ICarRepository, CarRepository>();

            services.AddTransient<ICarModelHandler, CarModelHandler>();
            services.AddTransient<ICarModelsRepository, CarModelsRepository>();

            services.AddTransient<IBrandHandler, BrandHandler>();
            services.AddTransient<IBrandRepository, BrandRepository>();

            services.AddTransient<ITicketHandler, TicketHandler>();
            services.AddTransient<ITicketRepository, TicketRepository>();

            services.AddTransient<IUserHandler, UserHandler>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<ICarImageHandler, CarImageHandler>();
            services.AddTransient<ICarImageRepository, CarImageRepository>();

            services.AddTransient<IAccountHandler, AccountHandler>();
            services.AddTransient<IAccountRepository, AccountRepository>();

            services.AddTransient<ICouponHandler, CouponHandler>();
            services.AddTransient<ICouponRepository, CouponRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "My API");
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            //app.UseSwaggerUI(c =>
            //{
            //    // To deploy on IIS
            //    c.SwaggerEndpoint("/webapi/swagger/v1/swagger.json", "Web API V1");
            //    //c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            //});


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseApiExceptionHandling();

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            
        }
    }
}

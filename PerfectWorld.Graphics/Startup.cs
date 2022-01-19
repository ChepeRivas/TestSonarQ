using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PerfectWorld.Data.Helpers;
using PerfectWorld.Data.Interfaces;
using PerfectWorld.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectWorld.Graphics
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
            //Mapper
            var mapperConfig = new MapperConfiguration(m => {

                m.AddProfile(new MappingProfile());
                m.AddProfile(new MappingSecret());

            });

            IMapper mapper = mapperConfig.CreateMapper();
            ISendMail InfoMail = new SendMailClass(
             Mail: "godspw2021@hotmail.com",
             Password: "Diciembre2021",
             BuildBody: "",
             Subject: "Welcome to the Gods Perfect World community"
             );
            services.AddSingleton(mapper);
            services.AddSingleton(InfoMail);
            services.AddHttpContextAccessor();
            services.AddScoped<IWebUserHelper, WebUserHelper>();
            services.AddScoped<LoginRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<PasswordRepository>();
            services.AddControllersWithViews();
            services.AddCors();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
         .AddCookie();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Log}/{action=Login}/{id?}");
            });
        }
    }
}

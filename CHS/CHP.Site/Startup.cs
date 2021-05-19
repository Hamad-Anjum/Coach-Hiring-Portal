
using System;
using System.Net.Http;

using CHS.Infrastructure.ViewModels;
using CHS.Services.Authentication;
using CHS.Services.Handler;
using CHS.Services.IService;
using CHS.Services.Service;

using MatBlazor;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CHP.Site
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMatBlazor();
            services.AddSignalR();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddMvc();
            services.AddServerSideBlazor(config =>
            {
                config.DetailedErrors = true;
            });

            //services.Configure<IISServerOptions>(options =>
            //{
            //    options.AutomaticAuthentication = false;
            //});

            services.ConfigureApplicationCookie(o =>
            {
                o.Cookie.Expiration = TimeSpan.FromHours(7);
                //var authProperties = new AuthenticationProperties
                //{
                //    IsPersistent = true,
                //    RedirectUri = Constant.ApiUrl,
                //    //Set Your Expire Cookie
                //    ExpiresUtc = DateTime.UtcNow.AddSeconds(10)
                //};
            });

            services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            services.AddHttpClient<IUserService, UserService>(x =>
            {
                x.BaseAddress = new Uri(Constant.ApiUrl);

                x.DefaultRequestHeaders.Add("User-Agent", "BlazorServer");
            });

            services.AddTransient<IFileUpload, FileUpload>();

            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IRefreshService, RefreshService>();
            services.AddHttpClient<IHttpService, HttpService>().AddHttpMessageHandler<ValidateHeaderHandler>();

            services.AddTransient<ValidateHeaderHandler>();

            services.AddSingleton<HttpClient>();

            // Add auth services
            services.AddOptions();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}

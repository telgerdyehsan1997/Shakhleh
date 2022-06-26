namespace Website
{
    using Domain;
    using Hangfire;
    using Hangfire.Dashboard;
    using Hangfire.MemoryStorage;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.OpenApi.Models;
    using Olive;
    using Olive.Email;
    using Olive.Entities.Data;
    using Olive.Hangfire;
    using Olive.Mvc.Testing;
    using Olive.PDF;
    using Swashbuckle.AspNetCore.Filters;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using Website.API;

    public class Startup : Olive.Mvc.Startup
    {
        private const int DefaultCookieTimeout = 30;
        IConfiguration _config;
        IWebHostEnvironment _env;
        public Startup(IWebHostEnvironment env, IConfiguration config, ILoggerFactory loggerFactory)
            : base(env, config, loggerFactory)
        {

            _config = config;
            _env = env;
        }

        protected override CultureInfo GetRequestCulture() => new CultureInfo("en-GB");

        public override void Configure(IApplicationBuilder app)
        {
            app.UseSession();
            base.Configure(app);

            if (_config.GetValue<bool>("Automated.Tasks:Enabled"))
            {
                //app.UseScheduledTasks<TaskManager>();
                app.UseScheduledTasks<CustomTaskManager>();
            }

            var path = Path.Combine(_env.ContentRootPath);
            using (var provider = new PhysicalFileProvider(path))
            {
                var fileOptions = new StaticFileOptions
                {
                    RequestPath = "",
                    FileProvider = provider,
                };
                app.UseStaticFiles(fileOptions);
            }
           
            //if (!Environment.IsDevelopment())
            //    app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Channel Ports Shipment API");
            });

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            });
            app.UseAuthorization();
            app.UseAuthentication();
            var options = new MemoryStorageOptions
            {
                FetchNextJobTimeout = 1.Days()
            };
            GlobalConfiguration.Configuration.UseMemoryStorage(options);

            app.UseDeveloperExceptionPage();
        }
        //protected override void ConfigureAuthCookie(CookieAuthenticationOptions options)
        //{
        //    base.ConfigureAuthCookie(options);

        //    options.Cookie.Domain = Config.Get("ChannelPorts:Cookie.Domain");
        //    var defaultTimeOut = Config.Get("ChannelPorts:Cookie.Timeout").TryParseAs<int>() ?? DefaultCookieTimeout;
        //    options.ExpireTimeSpan = defaultTimeOut.Minutes();
        //    options.Cookie.MaxAge = defaultTimeOut.Minutes();
        //    options.SlidingExpiration = true;
        //}
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
        // Set a short timeout for easy testing.
        var defaultTimeOut = Config.Get("ChannelPorts:Cookie.Timeout").TryParseAs<int>() ?? DefaultCookieTimeout;
                options.IdleTimeout = defaultTimeOut.Minutes();
                options.Cookie.HttpOnly = true;
        // Make the session cookie essential
        options.Cookie.IsEssential = true;
            });


            services.AddDataAccess(x => x.SqlServer());

            services.AddDatabaseLogger();
            services.AddScheduledTasks();
            services.AddEmail();

            if (Environment.IsDevelopment())
                services.AddDevCommands(x => x.AddTempDatabase<SqlServerManager, ReferenceData>());

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,

                            },
                            new List<string>()
                        }
                    });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Channel Ports Shipment API",
                    Version = "v1",
                    Description = "API to create shipments",
                });
                c.SchemaFilter<CustomXmlSchemaFilter>();
            });

            services
                .AddMvc()
                .AddXmlSerializerFormatters();

            AddApplicationDependencies(services);
        }

        protected override void ConfigureAuthentication(AuthenticationBuilder auth)
        {
            base.ConfigureAuthentication(auth);
            auth.AddSocialAuth();
        }

        public override async Task OnStartUpAsync(IApplicationBuilder app)
        {
            await base.OnStartUpAsync(app);

        }

        public IServiceCollection AddApplicationDependencies(IServiceCollection services)
        {

            services.AddSingleton(typeof(IJWTProvider), typeof(JWTProvider));
            services.AddSingleton<ISmsService, SmsService>();
            services.AddTransient<IHtml2PdfConverter, Html2PdfConverter>();
            services.AddTransient<IArchiveLogService, ArchiveLogService>();

            return services;
        }

        //protected override void ConfigureAuthCookie(CookieAuthenticationOptions options)
        //{
        //   /admbase.ConfigureAuthCookie(options);
        //    options.Events = new CookieAuthenticationEvents
        //    {
        //        OnRedirectToAccessDenied = ReplaceRedirector(HttpStatusCode.Forbidden, context => options.Events.RedirectToAccessDenied(context)),
        //        OnRedirectToLogin = ReplaceRedirector(HttpStatusCode.Unauthorized, context => options.Events.RedirectToLogin(context))
        //    };
        //}

        //static Func<RedirectContext<CookieAuthenticationOptions>, Task> ReplaceRedirector(HttpStatusCode statusCode, Func<RedirectContext<CookieAuthenticationOptions>, Task> existingRedirector) =>
        //    context =>
        //    {
        //        if (context.Request.Path.StartsWithSegments("/api"))
        //        {
        //            context.Response.StatusCode = (int)statusCode;
        //            return Task.CompletedTask;
        //        }
        //        return existingRedirector(context);
        //    };
    }

    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
            //return context.Request.RemoteIpAddress.IsAnyOf(Config.Get("Automated.Tasks:Hangfire.IPAddresses").Split('|'));
        }
    }

    #region Show error screen even in production?
    // Uncomment the following:
    // protected override void ConfigureExceptionPage(IApplicationBuilder app)
    //    => app.UseDeveloperExceptionPage();
    #endregion
}

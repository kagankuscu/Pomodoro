using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PomodoroRacer.Backend.Application;
using PomodoroRacer.Backend.Domain;
using PomodoroRacer.Backend.Infrastructure;
using PomodoroRacer.Backend.Web;

namespace PomodoroRacer.Backend.Startup;

public class Startup
{
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment CurrentEnvironment { get; }

    readonly string DefaultOrigins = "_defaultOrigins";
    readonly string AllOrigins = "_allOrigins";

    public Startup(
        IConfiguration configuration,
        IWebHostEnvironment currentEnvironment)
    {
        Configuration = configuration;
        CurrentEnvironment = currentEnvironment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddDomain()
            .AddApplication(this.Configuration)
            .AddInfrastructure()
            .AddWeb()
            //.AddDatabaseDeveloperPageExceptionFilter()
            //.AddSignalR()
            .AddControllers();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "PomodoroRacer API",
                Version = "v1"
            });
        });

        var allowedCorsOrigin = (Configuration.GetSection("AllowedCorsOrigins:Url")
                .Get<string>() ?? "*")
            .Split(';', StringSplitOptions.RemoveEmptyEntries);

        services.AddCors(options =>
        {
            options.AddPolicy(name: DefaultOrigins,
                builder =>
                {
                    builder.WithOrigins(allowedCorsOrigin)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            options.AddPolicy(AllOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
    }

    public void Configure(IApplicationBuilder app,
                          IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint(
                "/swagger/v1/swagger.json", "PomodoroRacer API v1"));
            app.UseCors(AllOrigins);
        }
        else
        {
            app.UseCors(DefaultOrigins);
            app.UseHttpsRedirection();
        }

        app.UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            //   .UseValidationExceptionHandler()
            .UseHealthChecks("/health")
            .UseEndpoints(endpoints => endpoints.MapControllers());
            // .UseEndpoints(endpoints =>
            // {
            //     endpoints.MapHub<EngagementProgressHub>("/engagementprogresshub");
            // });

       // app.Initialize();
    }
}
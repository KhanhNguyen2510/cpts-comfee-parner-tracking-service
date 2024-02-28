using System.Net;
using System.Threading.RateLimiting;
using Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.RateLimiting;
using static Domain.CommonConstant.CommonConstant;

namespace Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            _ = services.AddSwagger();
            _ = services.AddRateLimitRequest();
            _ = services.AddHttpContextAccessor();
            _ = services.AddRouting(option => option.LowercaseUrls = true);
            return services;
        }

        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.ConfigureOptions<ConfigureSwaggerOptions>();
            return services;
        }
        private static IServiceCollection AddApiVersion(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader =
                ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                         new HeaderApiVersionReader("x-api-version"),
                                         new MediaTypeApiVersionReader("x-api-version"));
            });

            return services;
        }

        private static IServiceCollection AddRateLimitRequest(this IServiceCollection services)
        {
            _ = services.AddRateLimiter(
                config =>
                {
                    config.AddFixedWindowLimiter(policyName: "API", options =>
                    {
                        options.QueueLimit = 2;
                        options.PermitLimit = 2;
                        options.AutoReplenishment = true;
                        options.Window = TimeSpan.FromSeconds(5);
                        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    });

                    config.OnRejected = async (context, token) =>
                    {
                        //Result<string> responseModel = await Result<string>.FailAsync("Too Many Requests", (int)HttpStatusCode.TooManyRequests);
                        // context.HttpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                        // await context.HttpContext.Response.WriteAsJsonAsync(responseModel, token);
                    };
                });
            return services;
        }

        internal static IServiceCollection AddCorsAndPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddCors(policy =>
            {
                policy.AddPolicy(
                    CorsPolicy.CorsDefault,
                    opt => opt
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithExposedHeaders("location", "Content-Disposition", "Link", "X-Total-Count", "X-Limit")
                );
            });
            return services;
        }

        internal static IServiceCollection AddApiVersion(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                                new HeaderApiVersionReader("x-api-version"),
                                                                new MediaTypeApiVersionReader("x-api-version"));
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            return services;
        }



    }
}
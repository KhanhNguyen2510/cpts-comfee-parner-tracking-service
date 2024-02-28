using Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            _ = services.AddSwagger();
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
    }
}
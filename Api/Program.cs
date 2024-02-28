using Api;
using Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;

_ = services.AddWebServices();

_ = services.AddInfrastructureServices();

const string __myAllowAllOrigins = "_myAllowAllOrigins";
_ = services.AddCors(options =>
{
    options.AddPolicy(__myAllowAllOrigins,
        policyBuilder =>
        {
            policyBuilder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("location", "Content-Disposition", "Link", "X-Total-Count", "X-Limit");
        });
});

_ = services.AddControllers();

WebApplication app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
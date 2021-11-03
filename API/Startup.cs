using System.IO;
using API.Extensions;
using API.Helpers;
using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace API
{
  public class Startup
  {
    private readonly IConfiguration _config;
    public Startup(IConfiguration config)
    {
      _config = config;

    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton<IResponseCacheService, ResponseCacheService>();
      services.AddScoped<ITokenService, TokenService>();
      services.AddScoped<IOrderService, OrderService>();
      services.AddScoped<IPaymentService, PaymentService>();
      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddScoped<IProductRepository, ProductRepository>();
      services.AddScoped<IBasketRepository, BasketRepository>();
      services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
      services.AddAutoMapper(typeof(MappingProfiles));
      services.AddControllers();
      services.AddSwaggerDocumentation();

      services.AddSingleton<ConnectionMultiplexer>(c => {
        var config = ConfigurationOptions.Parse(_config.GetConnectionString("Redis"), true);
        return ConnectionMultiplexer.Connect(config);
      });

      services.AddDbContext<StoreContext>(db => 
        db.UseNpgsql(_config.GetConnectionString("DefaultConnection")));
      services.AddDbContext<AppIdentityDbContext>(db =>
      {
        db.UseNpgsql(_config.GetConnectionString("IdentityConnection"));
      });
      services.AddIdentityServices(_config);
      services.AddCors(opt => 
        {
          opt.AddPolicy("CorsPolicy", builder => 
          {
              builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("https://localhost:4200");
          });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseMiddleware<ExceptionMiddleware>();
      app.UseStatusCodePagesWithReExecute("/errors/{0}");
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
      }
      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseStaticFiles();
      app.UseStaticFiles(new StaticFileOptions
        {
          FileProvider = new PhysicalFileProvider(
              Path.Combine(Directory.GetCurrentDirectory(), "Content")
          ), RequestPath = "/content"
        });
      app.UseCors("CorsPolicy");
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapFallbackToController("Index", "Fallback");
      });
    }
  }
}

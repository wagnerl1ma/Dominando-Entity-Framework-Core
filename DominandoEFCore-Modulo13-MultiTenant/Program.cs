using DominandoEFCore_Modulo13_MultiTenant.Data;
using DominandoEFCore_Modulo13_MultiTenant.Data.Interceptors;
using DominandoEFCore_Modulo13_MultiTenant.Data.ModelFactory;
using DominandoEFCore_Modulo13_MultiTenant.Domain;
using DominandoEFCore_Modulo13_MultiTenant.Extensions;
using DominandoEFCore_Modulo13_MultiTenant.Middlewares;
using DominandoEFCore_Modulo13_MultiTenant.Provider;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace DominandoEFCore_Modulo13_MultiTenant
{
    public class Program
    {
        public static IConfiguration Configuration { get; }

        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<TenantData>();
            //builder.Services.AddScoped<StrategySchemaInterceptor>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // Estrategia 1 - Identificador na tabela
            //builder.Services.AddDbContext<ApplicationContext>(p => p.UseSqlServer("Data source=(localdb)\\mssqllocaldb; Initial Catalog=Tenant99;Integrated Security=true;")
            //    .LogTo(Console.WriteLine)
            //    .EnableSensitiveDataLogging());

            // Estrategia 2 - Schema
            //    builder.Services.AddDbContext<ApplicationContext>((provider, options) =>
            //    {
            //        options
            //       .UseSqlServer("Data source=(localdb)\\mssqllocaldb; Initial Catalog=Tenant99;Integrated Security=true;")
            //       .LogTo(Console.WriteLine)
            //       .ReplaceService<IModelCacheKeyFactory, StrategySchemaModelCacheKey>()
            //       .EnableSensitiveDataLogging();

            //        //var interceptor = provider.GetRequiredService<StrategySchemaInterceptor>();

            //        //options.AddInterceptors(interceptor);
            //});

            // Estrategia 3 - Banco de dados
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<ApplicationContext>(provider =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

                var httpContext = provider.GetService<IHttpContextAccessor>()?.HttpContext;
                var tenantId = httpContext?.GetTenantId();

                //var connectionString = Configuration.GetConnectionString(tenantId);
                var connectionString = Configuration.GetConnectionString("custom").Replace("_DATABASE_", tenantId);

                optionsBuilder
                    .UseSqlServer(connectionString)
                    //.UseSqlServer("Data source=(localdb)\\mssqllocaldb; Initial Catalog=TenantZ;Integrated Security=true;")
                    .LogTo(Console.WriteLine)
                    .EnableSensitiveDataLogging();

                return new ApplicationContext(optionsBuilder.Options);
            });



            var app = builder.Build();

            //DatabaseInitialize(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //app.UseMiddleware<TenantMiddleware>();


            app.MapControllers();

            app.Run();
        }

        //private static void DatabaseInitialize(IApplicationBuilder app) // criacao do banco de dados 
        //{
        //    using var db = app.ApplicationServices
        //        .CreateScope()
        //        .ServiceProvider
        //        .GetRequiredService<ApplicationContext>();

        //    db.Database.EnsureDeleted();
        //    db.Database.EnsureCreated();

        //    for (var i = 1; i <= 5; i++)
        //    {
        //        db.People.Add(new Person { Name = $"Person {i}" });
        //        db.Products.Add(new Product { Description = $"Product {i}" });
        //    }

        //    db.SaveChanges();
        //}
    }
}
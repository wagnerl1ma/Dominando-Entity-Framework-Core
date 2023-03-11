using DominandoEFCore_Modulo14_RepositoryUow.Data;
using DominandoEFCore_Modulo14_RepositoryUow.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DominandoEFCore_Modulo14_RepositoryUow
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;  //Ignorando Looping
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationContext>(p => p.UseSqlServer("Data source=(localdb)\\mssqllocaldb; DataBase=UoW; Integrated Security=true;")
                            .LogTo(Console.WriteLine)
                            .EnableSensitiveDataLogging());

            builder.Services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void IncializarBaseDeDados(IApplicationBuilder app) // criacao do banco de dados e populando
        {
            using var db = app.ApplicationServices   //capturando instancia do context
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<ApplicationContext>();

            if (db.Database.EnsureCreated())
            {
                // para cada 10 departamentos, 10 Colaboradores
                db.Departamentos.AddRange(Enumerable.Range(1, 10).Select(p => new Domain.Departamento 
                {
                    Descricao = $"Departamento - {p}",
                    Colaboradores = Enumerable.Range(1, 10).Select(x => new Domain.Colaborador
                    {
                        Nome = $"Colaborador: {x}/{p}"
                    }).ToList()
                }));

                db.SaveChanges();
            }
        }
    }
}
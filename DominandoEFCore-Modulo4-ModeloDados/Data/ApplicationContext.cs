using DominandoEFCore_Modulo4_ModeloDados.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;

namespace DominandoEFCore_Modulo4_ModeloDados.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Conversor> Conversores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Ator> Atores { get; set; }
        public DbSet<Filme> Filmes { get; set; }

        public DbSet<Documento> Documentos { get; set; }


        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Instrutor> Instrutores { get; set; }
        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<Dictionary<string, object>> Configuracoes => Set<Dictionary<string, object>>("Configuracoes");

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data source=(localdb)\\mssqllocaldb; Initial Catalog=DevIO-02;Integrated Security=true;pooling=true;";

            optionsBuilder
                .UseSqlServer(strConnection)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI"); //config codificaçao nivel global
            //RAFAEL -> rafael
            //Jõao -> Joao

            modelBuilder.Entity<Departamento>().Property(p=>p.Descricao).UseCollation("SQL_Latin1_General_CP1_CS_AS"); //config codificaçao para determinada propriedade

            modelBuilder
                .HasSequence<int>("MinhaSequencia", "sequencias")
                .StartsAt(1)
                .IncrementsBy(2)
                .HasMin(1)
                .HasMax(10)
                .IsCyclic(); //reinicia a sequencia para o valor minimo configurado

            modelBuilder.Entity<Departamento>().Property(p=>p.Id).HasDefaultValueSql("NEXT VALUE FOR sequencias.MinhaSequencia"); //utilizando a sequencia
            */

            ////Configurando Index
            //modelBuilder
            //    .Entity<Departamento>()
            //    //.HasIndex(p => p.Descricao) // indice com apenas um campo
            //    .HasIndex(p=> new { p.Descricao, p.Ativo}) //indice com mais de um campo, indice composto
            //    .HasDatabaseName("idx_meu_indice_composto") // nome do indice
            //    .HasFilter("Descricao IS NOT NULL") // especificar filtro para o indice : melhora a performance da consulta
            //    //.HasFillFactor(80)
            //    .IsUnique(); // indice unico para nao ser duplicado


            ////Propragacao de Dados
            //modelBuilder.Entity<Estado>().HasData(new[]   //HasData: Quando gerar uma migration irá incluir esses dados juntos // essa funcionalidade é util para quando for criar dados que nao seja alterado com frequencia
            //{
            //    new Estado { Id = 1, Nome = "Sao Paulo"},
            //    new Estado { Id = 2, Nome = "Sergipe"}
            //});


            //Esquemas
            //modelBuilder.HasDefaultSchema("cadastros"); // configurando esquema global
            //modelBuilder.Entity<Estado>().ToTable("Estados", "SegundoEsquema"); // configurando para uma entidade especifica

            //var conversao = new ValueConverter<Versao, string>(p => p.ToString(), p => (Versao)Enum.Parse(typeof(Versao), p));

            //Convertendo Enum para String
            var conversao1 = new EnumToStringConverter<Versao>();

            modelBuilder.Entity<Conversor>()
                .Property(p => p.Versao)
                .HasConversion(conversao1);
            
            //.HasConversion(conversao);
            //.HasConversion(p => p.ToString(), p => (Versao)Enum.Parse(typeof(Versao), p));
            //.HasConversion<string>();

            /*
            modelBuilder.Entity<Conversor>()
                .Property(p => p.Status)
                .HasConversion(new Curso.Conversores.ConversorCustomizado());

            modelBuilder.Entity<Departamento>().Property<DateTime>("UltimaAtualizacao");
            */

            //modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

            modelBuilder.SharedTypeEntity<Dictionary<string, object>>("Configuracoes", b =>
            {
                b.Property<int>("Id");

                b.Property<string>("Chave")
                    .HasColumnType("VARCHAR(40)")
                    .IsRequired();

                b.Property<string>("Valor")
                    .HasColumnType("VARCHAR(255)")
                    .IsRequired();
            });
        }
    }
}
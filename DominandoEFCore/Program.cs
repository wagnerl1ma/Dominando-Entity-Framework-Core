// See https://aka.ms/new-console-template for more information

using DominandoEFCore.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

#region Main

Console.WriteLine("Hello, World!");

//EnsureCreatedAndDeleted();
//GapDoEnsureCreated();


#endregion


static void EnsureCreatedAndDeleted()
{
    using var db = new ApplicationContext();
    //db.Database.EnsureCreated(); // cria o banco de dados com os dbsets e string de conexao configurados caso nao exitir
    db.Database.EnsureDeleted(); // exclui o banco de dados com os dbsets e string de conexao configurados casos existir
}

static void GapDoEnsureCreated()
{
    //método para excecutar a criacao de bancos de dados em contextos diferentes
    using var db1 = new ApplicationContext();
    using var db2 = new ApplicationContextCidade();

    db1.Database.EnsureCreated();
    db2.Database.EnsureCreated();

    var databaseCreator = db2.GetService<IRelationalDatabaseCreator>();
    databaseCreator.CreateTables();
}
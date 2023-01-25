using DominandoEFCore_Modulo8_Transacoes.Data;
using DominandoEFCore_Modulo8_Transacoes.Domain;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace DominandoEFCore_Modulo8_Transacoes
{
    class Program
    {
        static void Main(string[] args)
        {
            ComportamentoPadrao();
            //GerenciandoTransacaoManualmente();
            //ReverterTransacao();
            //TransactionScope();
        }    

        static void TransactionScope()
        {
            CadastrarLivro();

            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                //Timeout= TimeSpan.FromSeconds(10), //definindo timeout para esse escopo
            };

            using(var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                ConsultarAtualizar();
                CadastraLivroEnterprise();
                CadastrarLivroDominandoEFCore();

                scope.Complete();
            }             
        } 

        static void ConsultarAtualizar()
        {
            using (var db = new ApplicationContext())
            {    
                var livro = db.Livros.FirstOrDefault(p=>p.Id == 1);
                livro.Autor = "Rafael Almeida"; 
                db.SaveChanges();
            }
        }

        static void CadastraLivroEnterprise()
        {
            using (var db = new ApplicationContext())
            {                  
                db.Livros.Add(
                    new Livro
                    {
                        Titulo = "ASP.NET Core Enterprise Applications",
                        Autor = "Eduardo Pires"
                    });  
                db.SaveChanges();    
            }
        }

        static void CadastrarLivroDominandoEFCore()
        {
            using (var db = new ApplicationContext())
            {   
                db.Livros.Add(
                    new Livro
                    {
                        Titulo = "Dominando o Entity Framework Core",
                        Autor = "Rafael Almeida"
                    });  
                db.SaveChanges();      
            }
        }

        static void SalvarPontoTransacao()
        {
            CadastrarLivro();

            using (var db = new ApplicationContext())
            {                 
                using var transacao = db.Database.BeginTransaction();

                try
                {
                    var livro = db.Livros.FirstOrDefault(p=>p.Id == 1);
                    livro.Autor = "Rafael Almeida"; 
                    db.SaveChanges();

                    transacao.CreateSavepoint("desfazer_apenas_insercao"); //Iniciando ponto de salvamento

                    db.Livros.Add(
                        new Livro
                        {
                            Titulo = "ASP.NET Core Enterprise Applications",
                            Autor = "Eduardo Pires"
                        });  
                    db.SaveChanges();    

                    db.Livros.Add(
                        new Livro
                        {
                            Titulo = "Dominando o Entity Framework Core",
                            Autor = "Rafael Almeida".PadLeft(16,'*') //forçando exception
                        });  
                    db.SaveChanges();     

                    transacao.Commit();             //FIm do ponto de salvamento
                }
                catch(DbUpdateException e)
                {
                    transacao.RollbackToSavepoint("desfazer_apenas_insercao"); // executando RollbackToSavepoint a partir do ponto de salvamento

                    if (e.Entries.Count(p=>p.State == EntityState.Added) == e.Entries.Count)
                    {
                        transacao.Commit();
                    }
                }
                
            }
        } 

        static void ReverterTransacao() // Revertendo transacao (RollBack) 
        {
            CadastrarLivro();

            using (var db = new ApplicationContext())
            {                 
                var transacao = db.Database.BeginTransaction();

                try
                {
                    var livro = db.Livros.FirstOrDefault(p=>p.Id == 1);
                    livro.Autor = "Rafael Almeida"; 
                    db.SaveChanges();

                    db.Livros.Add(
                        new Livro
                        {
                            Titulo = "Dominando o Entity Framework Core",
                            Autor = "Rafael Almeida".PadLeft(16,'*')  //forçando exception
                        }); 

                    db.SaveChanges();     

                    transacao.Commit();            
                }
                catch(Exception e)
                {
                    transacao.Rollback();
                }
                
            }
        }

        static void GerenciandoTransacaoManualmente()
        {
            CadastrarLivro();

            using (var db = new ApplicationContext())
            {                 
                var transacao = db.Database.BeginTransaction(); //Iniciando Transacao Manual

                var livro = db.Livros.FirstOrDefault(p=>p.Id == 1);
                livro.Autor = "Rafael Almeida"; 
                db.SaveChanges();

                Console.ReadKey();

                db.Livros.Add(
                    new Livro
                    {
                        Titulo = "Dominando o Entity Framework Core",
                        Autor = "Wagner Lima"
                    }); 

                db.SaveChanges();     

                transacao.Commit();   //Finalizando Transacao Manual          
            }
        }

        static void ComportamentoPadrao()
        {
            CadastrarLivro();

            using (var db = new ApplicationContext())
            {                 
                var livro = db.Livros.FirstOrDefault(p=>p.Id == 1);
                livro.Autor = "Rafael Almeida"; 

                db.Livros.Add(
                    new Livro
                    {
                        Titulo = "Dominando o Entity Framework Core",
                        Autor = "Rafael Almeida"
                    }); 

                db.SaveChanges();                 
            }
        }

        static void CadastrarLivro()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Livros.Add(
                    new Livro
                    {
                        Titulo = "Introdução ao Entity Framework Core",
                        Autor = "Rafael"
                    }); 

                db.SaveChanges();
            }
        }
    }
}

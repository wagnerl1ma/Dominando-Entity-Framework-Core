using DominandoEFCore_Modulo17_Final.Data;
using System.Diagnostics;

namespace DominandoEFCore_Modulo17_Final
{
    //Modulos: Sobrescrevendo comportamentos do EF Core e Diagnostics
    class Program
    {
        static void Main(string[] args)
        {
            DiagnosticListener.AllListeners.Subscribe(new MyInterceptorListener());

            using var db = new ApplicationContext();
            db.Database.EnsureCreated();

            //var sql = db.Departamentos.Where(p=>p.Id > 0).ToQueryString();

            _ = db.Departamentos.Where(p => p.Id > 0).ToArray();

            //Console.WriteLine(sql);
        }
    }
}

using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DominandoEFCore_Modulo10_Perfomance.Interceptadores
{
    public class InterceptadorPersistencia : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData, 
            InterceptionResult<int> result)
        {
            
            System.Console.WriteLine(eventData.Context.ChangeTracker.DebugView.LongView);

            return result;
        }
    }
}
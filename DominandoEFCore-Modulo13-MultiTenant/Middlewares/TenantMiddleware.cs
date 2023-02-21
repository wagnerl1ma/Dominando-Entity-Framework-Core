using DominandoEFCore_Modulo13_MultiTenant.Extensions;
using DominandoEFCore_Modulo13_MultiTenant.Provider;

namespace DominandoEFCore_Modulo13_MultiTenant.Middlewares
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var tenant = httpContext.RequestServices.GetRequiredService<TenantData>();

            tenant.TenantId = httpContext.GetTenantId();

            await _next(httpContext);
        }
    }
}
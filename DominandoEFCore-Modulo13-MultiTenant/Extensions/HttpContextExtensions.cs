using Microsoft.AspNetCore.Http;

namespace DominandoEFCore_Modulo13_MultiTenant.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetTenantId(this HttpContext httpContext)
        {
            // desenvolvedor.io/tenant-1/product -> " " / "tenant-1" / "product"
            // desenvolvedor.io/product/?tenantId=tenant-1

            var tenant = httpContext.Request.Path.Value.Split('/', StringSplitOptions.RemoveEmptyEntries)[0];

            return tenant;
        }

    }
}
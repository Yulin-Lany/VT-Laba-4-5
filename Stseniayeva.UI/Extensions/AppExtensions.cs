using Stseniayeva.UI.MiddleWare;
using Microsoft.AspNetCore.Builder;

namespace Stseniayeva.UI.Extensions
{
    public static class AppExtensions
    {
        public static IApplicationBuilder UseFileLogging(this IApplicationBuilder app) => app.UseMiddleware<FileLogger>();
    }
}

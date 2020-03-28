using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace WebPlayground.Setup
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                { 
                    Version = "v1",
                    Title = "WebPlayground - Case Study",
                    Contact = new OpenApiContact()
                    {
                        Name = "Felipe Coelho Machado",
                        Email = "felipecmachado@outlook.com"
                    },
                    License = new OpenApiLicense { Name = "MIT", Url = new System.Uri("https://github.com/felipecmachado/WebPlayground/blob/master/LICENSE") }
                });
            });
        }
    }
}

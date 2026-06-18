using Firma.Interfaces.CMS;
using Firma.Services.CMS;

namespace Firma.PortalWWW
{
    public static class DependencyInjectionFactory
    {

        public static void Resolve(IServiceCollection services,IConfiguration conf)
        {
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IPageService, PageService>();
        }

    }
}

using Firma.Interfaces.CMS;
using Firma.Interfaces.Hotel;
using Firma.Services.CMS;
using Firma.Services.Hotel;

namespace Firma.PortalWWW
{
    public static class DependencyInjectionFactory
    {

        public static void Resolve(IServiceCollection services,IConfiguration conf)
        {
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IRoomTypeService, RoomTypeService>();
        }

    }
}

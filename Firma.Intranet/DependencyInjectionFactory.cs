using Firma.Interfaces.CMS;
using Firma.Interfaces.Hotel;
using Firma.Services.CMS;
using Firma.Services.Hotel;

namespace Firma.Intranet
{
    public static class DependencyInjectionFactory
    {

        public static void Resolve(IServiceCollection services, IConfiguration conf)
        {
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IRoomTypeService, RoomTypeService>();
            services.AddScoped<IAmenityService, AmenityService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IGuestService, GuestService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IExportService, ExportService>();
            services.AddScoped<IPaymentService, PaymentService>();
        }
    }
}

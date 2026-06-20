using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Interfaces.Hotel
{
    public interface IExportService
    {

        Task<byte[]> ExportAllReservations();



    }
}

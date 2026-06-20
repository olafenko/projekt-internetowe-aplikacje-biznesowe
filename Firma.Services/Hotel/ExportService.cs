using ClosedXML.Excel;
using Firma.Data.Data;
using Firma.Interfaces.Hotel;
using Firma.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Services.Hotel
{
    public class ExportService : BaseService, IExportService
    {
        public ExportService(FirmaContext context) : base(context)
        {
        }

        public async Task<byte[]> ExportAllReservations()
        {

            var reservations = await _context.Reservation
                .Include(r => r.Room).ThenInclude(r => r.RoomType)
                .Include(r=>r.Guest)
                .OrderByDescending(r=> r.CheckInDate)
                .ToListAsync();

            using (var workbook = new XLWorkbook())
            {

                var worksheet = workbook.Worksheets.Add($"Rezerwacje - {DateTime.Today:dd.MM.yyyy}");
                worksheet.Cell("A1").Value = "Gość";
                worksheet.Cell("B1").Value = "Pokój";
                worksheet.Cell("C1").Value = "Data od";
                worksheet.Cell("D1").Value = "Data do";
                worksheet.Cell("E1").Value = "Kwota całkowita";
                worksheet.Cell("F1").Value = "Status";

                worksheet.Range("A1:F1").Style.Font.Bold = true;

                var currentRow = 2;

                foreach (var reservation in reservations)
                {

                    worksheet.Cell(currentRow, 1).Value = $"{reservation.Guest.Name} {reservation.Guest.LastName}";
                    worksheet.Cell(currentRow, 2).Value = reservation.Room.Number + $" ({reservation.Room.RoomType.Name})";
                    worksheet.Cell(currentRow, 3).Value = reservation.CheckInDate;
                    worksheet.Cell(currentRow, 4).Value = reservation.CheckOutDate;
                    worksheet.Cell(currentRow, 5).Value = reservation.TotalPrice;
                    worksheet.Cell(currentRow, 6).Value = reservation.ReservationStatus.ToString();

                    currentRow++;
                }

                worksheet.Column(3).Style.DateFormat.Format = "dd.MM.yyy HH:mm";
                worksheet.Column(4).Style.DateFormat.Format = "dd.MM.yyy HH:mm";
                worksheet.Column(5).Style.DateFormat.Format = "#,##0.00 ZŁ";

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }


            }

        }
    }
}

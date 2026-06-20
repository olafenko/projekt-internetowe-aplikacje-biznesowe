using Azure.Core;
using Firma.Data.Data;
using Firma.Data.Data.Hotel;
using Firma.Interfaces.Hotel;
using Firma.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Services.Hotel
{
    public class GuestService : BaseService, IGuestService
    {
        public GuestService(FirmaContext context) : base(context)
        {
        }

        public async Task<Guest> CreateNewGuest(string name, string lastName, string email, string phoneNumber, string country, string identityCardNumber)
        {
            var newGuest = new Guest
            {
                Name = name,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Country = country,
                IdentityCardNumber = identityCardNumber,
                IsActive = true
            };

            _context.Guest.Add(newGuest);
            await _context.SaveChangesAsync();

            return newGuest;
        }

        public async Task<Guest?> GetExistingGuestByIdentityCardNumber(string identityCardNumber)
        {
            return await _context.Guest.FirstOrDefaultAsync(g => g.IsActive && g.IdentityCardNumber == identityCardNumber);
        }
    }
}

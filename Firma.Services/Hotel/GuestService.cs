
using Firma.Data.Data;
using Firma.Data.Data.Hotel;
using Firma.Interfaces.Hotel;
using Firma.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

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

        public async Task DeleteGuest(int id)
        {

            var guest = await _context.Guest.FirstOrDefaultAsync(g => g.IsActive && g.Id == id);

            if (guest != null)
            {
                guest.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<Guest>> GetAllGuests()
        {
            return await _context.Guest.Where(g => g.IsActive).ToListAsync();
        }

        public async Task<Guest?> GetExistingGuestByIdentityCardNumber(string identityCardNumber)
        {
            return await _context.Guest.FirstOrDefaultAsync(g => g.IsActive && g.IdentityCardNumber == identityCardNumber);
        }

        public async Task<Guest?> GetGuestById(int id)
        {
            return await _context.Guest.FirstOrDefaultAsync(g => g.IsActive && g.Id == id);
        }

        public bool GuestExists(int id)
        {
            return _context.Guest.Any(g => g.IsActive && g.Id == id);
        }

        public async Task UpdateGuest(int id, string name, string lastName, string email, string phoneNumber, string country, string identityCardNumber, string notes)
        {
            var guest = await _context.Guest.FirstOrDefaultAsync(g => g.IsActive && g.Id == id);

            if (guest != null)
            {
                guest.Name = name;
                guest.LastName = lastName;
                guest.Email = email;
                guest.PhoneNumber = phoneNumber;
                guest.Country = country;
                guest.IdentityCardNumber = identityCardNumber;
                guest.Notes = notes;

                _context.Update(guest);
                await _context.SaveChangesAsync();
            }

        }
    }
}

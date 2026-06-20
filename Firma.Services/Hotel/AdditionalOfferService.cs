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
    public class AdditionalOfferService : BaseService, IAdditionalOfferService
    {
        public AdditionalOfferService(FirmaContext context) : base(context)
        {
        }

        public bool AdditionalOfferExists(int id)
        {
            return _context.Service.Any(o => o.IsActive && o.Id == id);
        }

        public async Task CreateAdditionalOffer(string name, string description, decimal price)
        {
            var offer = new AdditionalOffer
            {
                Name = name,
                Description = description,
                Price = price,
                IsActive = true
            };

            _context.Service.Add(offer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAdditionalOffer(int id)
        {
            var offer = await _context.Service.FirstOrDefaultAsync(o => o.IsActive && o.Id == id);

            if (offer != null)
            {
                offer.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<AdditionalOffer?> GetAdditionalOfferById(int id)
        {
            return await _context.Service.FirstOrDefaultAsync(o => o.IsActive && o.Id == id);
        }
        

        public async Task<IList<AdditionalOffer>> GetAllAdditionalOffers()
        {
            return await _context.Service.Where(o => o.IsActive).ToListAsync();
        }
        public async Task UpdateAdditionalOffer(int id, string name, string description, decimal price)
        {
            var offer = await _context.Service.FirstOrDefaultAsync(o => o.IsActive && o.Id == id);

            if (offer != null)
            {
                offer.Name = name;
                offer.Description = description;
                offer.Price = price;

                _context.Update(offer);
                await _context.SaveChangesAsync();
            }
        }

    }
}

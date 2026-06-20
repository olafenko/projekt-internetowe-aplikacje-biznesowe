using Firma.Data.Data.Hotel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Interfaces.Hotel
{
    public interface IAdditionalOfferService
    {

        Task<IList<AdditionalOffer>> GetAllAdditionalOffers();

        Task<AdditionalOffer?> GetAdditionalOfferById(int id);

        Task CreateAdditionalOffer(string name, string description, decimal price);

        Task UpdateAdditionalOffer(int id, string name, string description, decimal price);

        Task DeleteAdditionalOffer(int id);

        bool AdditionalOfferExists(int id);
    }
}

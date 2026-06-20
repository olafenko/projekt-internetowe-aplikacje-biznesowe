using Firma.Data.Data.Hotel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Interfaces.Hotel
{
    public interface IGuestService
    {

        Task<Guest?> GetExistingGuestByIdentityCardNumber(string identityCardNumber);
        Task<Guest> CreateNewGuest(string name, string lastName,string email, string phoneNumber,string country, string identityCardNumber);

    }
}

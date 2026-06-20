using Firma.Data.Data.Hotel;

namespace Firma.Interfaces.Hotel
{
    public interface IGuestService
    {

        Task<Guest?> GetExistingGuestByIdentityCardNumber(string identityCardNumber);
        Task<Guest> CreateNewGuest(string name, string lastName, string email, string phoneNumber, string country, string identityCardNumber);
        Task UpdateGuest(int id, string name, string lastName, string email, string phoneNumber, string country, string identityCardNumber, string notes);
        Task<IList<Guest>> GetAllGuests();
        Task<Guest?> GetGuestById(int id);
        bool GuestExists(int id);
        Task DeleteGuest(int id);

    }
}

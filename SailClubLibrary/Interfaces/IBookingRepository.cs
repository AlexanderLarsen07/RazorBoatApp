using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Interfaces
{
    /// <summary>
    /// Interface for the BookingRepository class
    /// </summary>
    public interface IBookingRepository
    {
        void AddBooking(Booking booking);
        void RemoveBooking(Booking b);
        List<Booking> GetAllBookings();
        void UpdateBooking(int id, Booking newBooking);
        void PrintAll();
        int GetBookingCountForMember(Member member);
        Dictionary<string, int> GetAllBookingsForMembers();
    }
}

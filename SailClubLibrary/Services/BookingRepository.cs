using SailClubLibrary.Exceptions;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Services
{
    /// <summary>
    /// Class for Constructing and calling Booking Repository Objects using the interface
    /// </summary>
    public class BookingRepository : IBookingRepository
    {
        #region Fields
        private List<Booking> _bookings;
        #endregion

        #region Constructors
        public BookingRepository()
        {
            _bookings = [];
        }
        #endregion

        #region Methods
        /// <summary>
        /// Removes a booking from the booking list
        /// </summary>
        public void RemoveBooking(Booking booking)
        {
            _bookings.Remove(booking);
        }

        /// <summary>
        /// Returns our booking list
        /// </summary>
        public List<Booking> GetAllBookings()
        {
            return _bookings;
        }

        /// <summary>
        /// "Updates" a booking in the booking list,
        /// by replacing it with a new booking, but keeping the old id
        /// </summary>
        public void UpdateBooking(int id, Booking newBooking)
        {
            for (int i = 0; i < _bookings.Count; i++)
            {
                if (_bookings[i].Id == id)
                {
                    newBooking.Id = id;
                    _bookings[i] = newBooking;
                    return;
                }
            }
        }

        /// <summary>
        /// Books a boat if all checks are passed,
        /// otherwise an exception is thrown.
        /// </summary>
        public void AddBooking(Booking booking)
        {
            Boat boat = booking.TheBoat;
            Member member = booking.TheMember;
            DateTime startDate = booking.StartDate;
            DateTime endDate = booking.EndDate;

            if ((boat == null || member == null))
            {
                throw new NullReferenceException("Mangler input");
            }

            if ((startDate >= endDate))
            {
                throw new InvalidDateException("Startdato skal være før slutdato.");
            }

            if (CheckBookingOverlaps(boat, startDate, endDate))
            {
                throw new OverlappingDateException("Bookingen overlapper med en anden.");
            }

            foreach (Booking b in _bookings)
            {
                if (b.Id == booking.Id)
                {
                    return;
                }
            }
            _bookings.Add(booking);
            Console.WriteLine("Båden er hermed blevet booket");
        }

        /// <summary>
        /// Returns the number of bookings a member currently has, if any
        /// </summary>
        public int GetBookingCountForMember(Member member)
        {
            int count = 0;
            foreach (Booking existingBooking in _bookings)
            {
                bool validMember = existingBooking.TheMember != null && existingBooking.TheMember.Id == member.Id;
                if (validMember)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Returns a dictionary with key being the member name
        /// and the value being their amount of current bookings
        /// </summary>
        public Dictionary<string, int> GetAllBookingsForMembers()
        {
            Dictionary<Member, int> memberCounts = [];
            foreach (Booking existingBooking in _bookings)
            {
                Member member = existingBooking.TheMember;
                if (member != null)
                {
                    if (!memberCounts.ContainsKey(member))
                    {
                        memberCounts[member] = 0;
                    }
                    memberCounts[member]++;
                }
            }
            Dictionary<string, int> result = [];
            foreach (KeyValuePair<Member, int> kvp in memberCounts)
            {
                result[kvp.Key.FirstName] = kvp.Value;
            }
            return result;
        }

        /// <summary>
        /// Prints all bookings in our booking list
        /// </summary>
        public void PrintAll()
        {
            foreach (Booking b in _bookings)
            {
                Console.WriteLine(b);
            }
        }

        /// <summary>
        /// Checks if a new boat booking date overlaps with an existing booking date
        /// of the same boat
        /// </summary>
        public bool CheckBookingOverlaps(Boat boat, DateTime startDate, DateTime endDate)
        {
            foreach (Booking existingBooking in _bookings)
            {
                Boat existingBoat = existingBooking.TheBoat;
                if (existingBoat == null)
                {
                    continue; //Skip null boats
                }
                bool matchingSailNum = existingBoat.SailNumber == boat.SailNumber;
                if (matchingSailNum)
                {
                    bool startsBeforeExistingEnds = startDate < existingBooking.EndDate;
                    bool endsAfterExistingStarts = endDate > existingBooking.StartDate;
                    bool overlaps = startsBeforeExistingEnds && endsAfterExistingStarts;
                    if (overlaps)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Returns all active bookings when called
        /// </summary>
        /// <returns></returns>
        public List<Booking> GetAllActiveBookings()
        {
            List<Booking> activeList = [];
            foreach (Booking b in _bookings)
            {
                if (b.IsActive)
                {
                    activeList.Add(b);
                }
            }
            return activeList;
        }
        #endregion
    }
}

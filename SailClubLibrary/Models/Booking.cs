using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Models
{
    public class Booking
    {
        #region Instance Fields

        private bool _isActive;
        #endregion
        #region Properties
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive
        {
            get
            {
                return StartDate <= DateTime.Now && DateTime.Now <= EndDate;
            }
        }
        public bool SailCompleted { get; set; }
        public string Destination { get; set; }
        public Member TheMember { get; set; }
        public Boat TheBoat { get; set; }
        #endregion
        #region Constructor
        public Booking(int id, DateTime startDate, DateTime endDate, string destination, Member member, Boat boat)
        {
            StartDate = startDate;
            EndDate = endDate;
            Destination = destination;
            Id = id;
            TheMember = member;
            TheBoat = boat;
        }
        public Booking()
        {
            
        }
        #endregion
        #region Methods
        public override string ToString()
        {
            return $"Id: {Id} " +
                $"\nStart Dato: {StartDate} " +
                $"\nSlut Dato: {EndDate} " +
                $"\nDestination: {Destination} " +
                $"\nBåden med sejlnummeret: {TheBoat.SailNumber}" +
                $"\nBooket af: {TheMember.FirstName}" +
                $"\nBåden er kommet i havn: {SailCompleted}";
        }
        #endregion
    }
}

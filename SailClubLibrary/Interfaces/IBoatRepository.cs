using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Interfaces
{
    /// <summary>
    /// Interface for the BoatRepository Class
    /// </summary>
    public interface IBoatRepository
    {
        #region Properties
        public int Count { get; }
        #endregion

        #region Methods
        List<Boat> GetAllBoats();
        void AddBoat(Boat boat);
        void RemoveBoat(string sailNumber);
        void UpdateBoat(Boat boat);
        Boat? SearchBoat(string sailNumber);
        List<Boat> FilterBoats(string filterCriteria);
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailClubLibrary.Models;

namespace SailClubLibrary.Helpers.Sorting
{
    public class BoatCompareBySailNumber : IComparer<Boat>
    {
        #region Methods
        public int Compare(Boat? x, Boat? y)
        {
            return x.Boat_Sailnumber.CompareTo(y.Boat_Sailnumber);
        }
        #endregion
    }
}

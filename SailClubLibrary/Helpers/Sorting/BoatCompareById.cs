using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailClubLibrary.Models;

namespace SailClubLibrary.Helpers.Sorting
{
    public class BoatCompareById : IComparer<Boat>
    {
        #region Methods
        public int Compare(Boat? x, Boat? y)
        {
            return x.Boat_ID.CompareTo(y.Boat_ID);
        }
        #endregion
    }
}

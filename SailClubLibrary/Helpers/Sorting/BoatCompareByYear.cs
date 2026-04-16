using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailClubLibrary.Models;

namespace SailClubLibrary.Helpers.Sorting
{
    public class BoatCompareByYear : IComparer<Boat>
    {
        #region Methods
        public int Compare(Boat? x, Boat? y)
        {
            return x.Boat_yearofconstruction.CompareTo(y.Boat_yearofconstruction);
        }
        #endregion
    }
}

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
        public int Compare(Boat? x, Boat? y)
        {
            return x.Id.CompareTo(y.Id);
        }
    }
}

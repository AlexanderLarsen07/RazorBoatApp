using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailClubLibrary.Helpers.Attributes;

namespace SailClubLibrary.Models
{
    /// <summary>
    /// Generic Class for Constructing Boat Objects using the interface
    /// </summary>
    public class Boat : IComparable<Boat>
    {
        #region Instance Fields

        #endregion

        #region Properties
        [Required(ErrorMessage ="Id is required")]
        [IgnoreUpdate]
        public int Boat_ID { get; set; }
        public BoatType Boat_TheBoatType { get; set; }
        public string Boat_Model { get; set; }
        [Required]
        public string Boat_Sailnumber { get; set; }
        public string Boat_EngineInfo { get; set; }
        public double Boat_Draft { get; set; }
        public double Boat_Width { get; set; }
        public double Boat_Length { get; set; }
        public string Boat_yearofconstruction { get; set; }

        #endregion

        #region Constructors
        public Boat()
        {

        }

        public Boat(int id, BoatType boatType, string model, string sailNumber, string engineInfo,
            double draft, double width, double length, string yearOfConstruction)
        {
            Boat_ID = id;
            Boat_TheBoatType = boatType;
            Boat_Model = model;
            Boat_Sailnumber = sailNumber;
            Boat_EngineInfo = engineInfo;
            Boat_Draft = draft;
            Boat_Width = width;
            Boat_Length = length;
            Boat_yearofconstruction = yearOfConstruction;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a writeline featuring the contents of the object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ($"\nBåd Nr.{Boat_ID}: " +
                $"\nBådinfo..." +
                $"\n{Boat_yearofconstruction} {Boat_Model} {Boat_TheBoatType} {Boat_Sailnumber} " +
                $"\nMotorinfo: {Boat_EngineInfo} " +
                $"\nDimensioner... " +
                $"\nDybgang: {Boat_Draft}, Bredde: {Boat_Width}, Længde: {Boat_Length}");
        }

        public string FilterAll()
        {
            return $"{Boat_TheBoatType.ToString() ?? ""} {Boat_Model ?? ""} {Boat_Sailnumber ?? ""} {Boat_EngineInfo ?? ""} {Boat_yearofconstruction ?? ""}";
        }

        public int CompareTo(Boat? boat)
        {
            if (boat == null) return 1;
            return Boat_ID.CompareTo(boat.Boat_ID);
        }
        #endregion
    }
}

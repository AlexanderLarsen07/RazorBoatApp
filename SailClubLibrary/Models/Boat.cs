using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int Id { get; set; }
        public BoatType TheBoatType { get; set; }
        public string Model { get; set; }
        [Required]
        public string SailNumber { get; set; }
        public string EngineInfo { get; set; }
        public double Draft { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public string YearOfConstruction { get; set; }

        #endregion
        public Boat()
        {

        }

        #region Constructor
        public Boat(int id, BoatType boatType, string model, string sailNumber, string engineInfo,
            double draft, double width, double length, string yearOfConstruction)
        {
            Id = id;
            TheBoatType = boatType;
            Model = model;
            SailNumber = sailNumber;
            EngineInfo = engineInfo;
            Draft = draft;
            Width = width;
            Length = length;
            YearOfConstruction = yearOfConstruction;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Returns a writeline featuring the contents of the object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ($"\nBåd Nr.{Id}: " +
                $"\nBådinfo..." +
                $"\n{YearOfConstruction} {Model} {TheBoatType} {SailNumber} " +
                $"\nMotorinfo: {EngineInfo} " +
                $"\nDimensioner... " +
                $"\nDybgang: {Draft}, Bredde: {Width}, Længde: {Length}");
        }

        public int CompareTo(Boat? boat)
        {
            if (boat == null) return 1;
            return Id.CompareTo(boat.Id);
        }
        #endregion

    }
}

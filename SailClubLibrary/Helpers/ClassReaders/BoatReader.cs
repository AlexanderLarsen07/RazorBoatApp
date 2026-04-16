using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SailClubLibrary.Models;

namespace SailClubLibrary.Helpers.ClassReaders
{
    public class BoatReader
    {
        #region Methods
        /// <summary>
        /// Reads a <see cref="Boat"/> entity from a <see cref="SqlDataReader"/>.
        /// </summary>
        /// <param name="sqlDataReader">
        /// The data reader containing a row with boat information.
        /// The expected columns are:
        /// Boat_ID, Boat_TheBoatType, Boat_Model, Boat_Sailnumber,
        /// Boat_EngineInfo, Boat_Draft, Boat_Width, Boat_Length, Boat_yearofconstruction.
        /// </param>
        /// <returns>
        /// A <see cref="Boat"/> object populated with values from the current row of the data reader.
        /// </returns>
        /// <exception cref="InvalidCastException">
        /// Thrown if a column value cannot be converted to the expected type.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if any of the required column names are missing from the data reader.
        /// </exception>
        /// <remarks>
        /// This method assumes that the <paramref name="sqlDataReader"/> is positioned at a valid row.
        /// It also assumes that all required columns are present and contain non-null values.
        /// </remarks>
        public static Boat Reader(SqlDataReader sqlDataReader)
        {
            int boatID = sqlDataReader.GetInt32("Boat_ID");
            BoatType boatType = Enum.GetValues<BoatType>()[sqlDataReader.GetInt32("Boat_TheBoatType")];
            string boatModel = sqlDataReader.GetString("Boat_Model");
            string sailNumber = sqlDataReader.GetString("Boat_Sailnumber");
            string boatEngineInfo = sqlDataReader.GetString("Boat_EngineInfo");
            double boatDraft = (double)sqlDataReader.GetDecimal("Boat_Draft");
            double boatWidth = (double)sqlDataReader.GetDecimal("Boat_Width");
            double boatLength = (double)sqlDataReader.GetDecimal("Boat_Length");
            string yearOfConstruction = sqlDataReader.GetInt32("Boat_yearofconstruction").ToString();

            return new Boat(boatID, boatType, boatModel, sailNumber, boatEngineInfo, boatDraft, boatWidth, boatLength, yearOfConstruction);
        }
        #endregion
    }
}

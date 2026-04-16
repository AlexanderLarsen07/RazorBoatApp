using SailClubLibrary.Exceptions;
using SailClubLibrary.Models;
using SailClubLibrary.Services;

namespace SailClubLibraryUnitTests
{
    [TestClass]
    public sealed class BoatRepoUnitTest
    {
        [TestMethod]
        public void TestBoat_AddBoat_Success()
        {
            BoatRepository boatRepository = new();
            Boat boat = new Boat(12, BoatType.TERA, "fds", "21", "13", 231, 123, 21, "2132");

            boatRepository.AddBoat(boat);

            Assert.AreSame(boatRepository.SearchBoat(boat.Boat_Sailnumber), boat);
        }
        [TestMethod]
        public void TestBoat_AddBoat_Exception()
        {
            BoatRepository boatRepository = new();
            Boat boat = new Boat(12, BoatType.TERA, "fds", "21", "13", 231, 123, 21, "2132");

            boat.Boat_Sailnumber = "16-3335";

            Assert.ThrowsException<BoatSailnumberExistsException>(() => boatRepository.AddBoat(boat));
        }
        [TestMethod]
        public void TestBoat_RemoveBoat_RemovedFromRepo()
        {
            BoatRepository boatRepository = new();

            boatRepository.RemoveBoat("16-3335");

            Assert.IsFalse(boatRepository.GetAllBoats().Any(b => b.Boat_Sailnumber == "16-3335"));
        }
        [TestMethod]
        public void TestBoat_UpdateBoat_ValuesTransferred()
        {
            BoatRepository boatRepository = new();
            Boat updatedBoat = new Boat(12, BoatType.TERA, "fds", "21", "1sdfkjhbhjf", 231, 123, 21, "2132");
            updatedBoat.Boat_Sailnumber = "16-3335";

            boatRepository.UpdateBoat(updatedBoat);
            Boat existingBoat = boatRepository.SearchBoat("16-3335");

            Assert.IsNotNull(existingBoat);
            Assert.AreEqual(existingBoat.Boat_TheBoatType, updatedBoat.Boat_TheBoatType);
            Assert.AreEqual(existingBoat.Boat_Model, updatedBoat.Boat_Model);
            Assert.AreEqual(existingBoat.Boat_EngineInfo, updatedBoat.Boat_EngineInfo);
            Assert.AreEqual(existingBoat.Boat_Draft, updatedBoat.Boat_Draft);
            Assert.AreEqual(existingBoat.Boat_Width, updatedBoat.Boat_Width);
            Assert.AreEqual(existingBoat.Boat_Length, updatedBoat.Boat_Length);
            Assert.AreEqual(existingBoat.Boat_yearofconstruction, updatedBoat.Boat_yearofconstruction);
        }
        [TestMethod]
        public void TestBoat_SearchBoat_NotNull()
        {
            BoatRepository boatRepository = new();

            Assert.IsNotNull(boatRepository.SearchBoat("16-3335"));
        }
        [TestMethod]
        public void TestBoat_SearchBoat_Null()
        {
            BoatRepository boatRepository = new();

            Assert.IsNull(boatRepository.SearchBoat("bsugrq"));
        }
        [TestMethod]
        public void TestBoat_SearchBoat_ReturnsCorrectBoat()
        {
            BoatRepository boatRepository = new();

            Boat boat = boatRepository.SearchBoat("16-3335");

            Assert.IsNotNull(boat);
            Assert.AreEqual("16-3335", boat.Boat_Sailnumber);
        }
    }
}
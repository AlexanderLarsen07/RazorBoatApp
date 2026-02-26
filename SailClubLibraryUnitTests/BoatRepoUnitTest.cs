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

            Assert.AreSame(boatRepository.SearchBoat(boat.SailNumber), boat);
        }
        [TestMethod]
        public void TestBoat_AddBoat_Exception()
        {
            BoatRepository boatRepository = new();
            Boat boat = new Boat(12, BoatType.TERA, "fds", "21", "13", 231, 123, 21, "2132");

            boat.SailNumber = "16-3335";

            Assert.ThrowsException<BoatSailnumberExistsException>(() => boatRepository.AddBoat(boat));
        }
        [TestMethod]
        public void TestBoat_RemoveBoat_RemovedFromRepo()
        {
            BoatRepository boatRepository = new();

            boatRepository.RemoveBoat("16-3335");

            Assert.IsFalse(boatRepository.GetAllBoats().Any(b => b.SailNumber == "16-3335"));
        }
        [TestMethod]
        public void TestBoat_UpdateBoat_ValuesTransferred()
        {
            BoatRepository boatRepository = new();
            Boat updatedBoat = new Boat(12, BoatType.TERA, "fds", "21", "1sdfkjhbhjf", 231, 123, 21, "2132");
            updatedBoat.SailNumber = "16-3335";

            boatRepository.UpdateBoat(updatedBoat);
            Boat existingBoat = boatRepository.SearchBoat("16-3335");

            Assert.IsNotNull(existingBoat);
            Assert.AreEqual(existingBoat.TheBoatType, updatedBoat.TheBoatType);
            Assert.AreEqual(existingBoat.Model, updatedBoat.Model);
            Assert.AreEqual(existingBoat.EngineInfo, updatedBoat.EngineInfo);
            Assert.AreEqual(existingBoat.Draft, updatedBoat.Draft);
            Assert.AreEqual(existingBoat.Width, updatedBoat.Width);
            Assert.AreEqual(existingBoat.Length, updatedBoat.Length);
            Assert.AreEqual(existingBoat.YearOfConstruction, updatedBoat.YearOfConstruction);
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
            Assert.AreEqual("16-3335", boat.SailNumber);
        }
    }
}
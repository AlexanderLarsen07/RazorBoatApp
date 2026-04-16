using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SailClubLibrary.Helpers.ClassReaders;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Boats
{
    public class UpdateBoatModel : PageModel
    {
        #region Instance fields
        private IGenericRepositoryAsync<Boat> _repo;

        private string sqlSelect = "SELECT * FROM Boat WHERE Boat_Sailnumber = @sailNumber";
        private string sqlUpdate = @"UPDATE Boat SET Boat_TheBoatType = @Boat_TheBoatType, Boat_Model = @Boat_Model, Boat_Sailnumber = @Boat_Sailnumber, Boat_EngineInfo = @Boat_EngineInfo, Boat_Draft = @Boat_Draft, Boat_Width = @Boat_Width, Boat_Length = @Boat_Length, Boat_yearofconstruction = @Boat_yearofconstruction WHERE Boat_ID = @Boat_ID";
        #endregion

        #region Properties
        [BindProperty]
        public Boat NewBoat { get; set; }
        #endregion

        #region Constructor
        public UpdateBoatModel(IGenericRepositoryAsync<Boat> boatRepository)
        {
            _repo = boatRepository;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> OnGet(string sailNumber)
        {
            NewBoat = await _repo.SearchObjectAsync(sqlSelect, BoatReader.Reader, cmd => cmd.Parameters.AddWithValue("@sailNumber", sailNumber));

            if (NewBoat == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            await _repo.UpdateObjectAsync(sqlUpdate, NewBoat, "Boat_ID", NewBoat.Boat_ID);

            return RedirectToPage("Index");
        }
        #endregion
    }
}

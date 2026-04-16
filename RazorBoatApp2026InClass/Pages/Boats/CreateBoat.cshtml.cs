using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Exceptions;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Boats
{
    public class CreateBoatModel : PageModel
    {
        #region Instance fields
        private IGenericRepositoryAsync<Boat> _repo;
        
        private string sqlAdd = "insert into Boat values(@Boat_TheBoatType, @Boat_Model, @Boat_Sailnumber, @Boat_EngineInfo, @Boat_Draft, @Boat_Width, @Boat_Length, @Boat_yearofconstruction)";
        #endregion

        #region Properties
        [BindProperty]
        public Boat NewBoat { get; set; }
        #endregion

        #region Constructor
        public CreateBoatModel(IGenericRepositoryAsync<Boat> boatRepository)
        {
            _repo = boatRepository;
        }
        #endregion

        #region Methods
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                _repo.AddObjectAsync(sqlAdd, NewBoat);
            }
            catch (BoatSailnumberExistsException bex)
            {
                ViewData["ErrorMessage"] = bex.Message;
                return Page();
            }
            catch(Exception exc)
            {
                ViewData["ErrorMessage"] = exc.Message;
            }
            return RedirectToPage("index");
        }
        #endregion
    }
}

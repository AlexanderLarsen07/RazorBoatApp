using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Boats
{
    public class UpdateBoatModel : PageModel
    {
        private IBoatRepository _repo;

        [BindProperty]
        public Boat NewBoat { get; set; }
        public UpdateBoatModel(IBoatRepository boatRepository)
        {
            _repo = boatRepository;
        }
        public IActionResult OnGet(string sailNumber)
        {
            NewBoat = _repo.SearchBoat(sailNumber);
            return Page();
        }
        public IActionResult OnPost()
        {
            _repo.UpdateBoat(NewBoat);
            return RedirectToPage("index");
        }
    }
}

using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Helpers.Sorting;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Boats
{
    public class IndexModel : PageModel
    {
        private IBoatRepository bRepo;

        public List<Boat> Boats { get; set; }

        //[BindProperty]
        //public Boat Boat { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }

        public IndexModel(IBoatRepository boatRepository)
        {
            bRepo = boatRepository;
        }
        public void OnGet()
        {
            if (!String.IsNullOrEmpty(FilterCriteria))
            {
                Boats = bRepo.FilterBoats(FilterCriteria);
            }
            else
            {
                Boats = bRepo.GetAllBoats();
            }
            Boats = BoatSort(Boats);
           
        }
       
        private List<Boat> BoatSort(List<Boat> boats)
        {
            switch (SortBy)
            {
                case "Id":
                    boats.Sort();
                    break;
                case "SailNumber":
                    boats.Sort(new BoatCompareBySailNumber());
                    break;
                case "YearOfConstruction":
                    boats.Sort(new BoatCompareByYear());
                    break;
                default:
                    break;
            }
            return boats;
        }
        public IActionResult OnPostDelete(string sailNumber)
        {
            bRepo.RemoveBoat(sailNumber);
            return RedirectToPage("index");
        }
    }
}

using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Helpers.ClassReaders;
using SailClubLibrary.Helpers.Filter;
using SailClubLibrary.Helpers.Sorting;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Boats
{
    public class IndexModel : PageModel
    {
        #region Instance fields
        private IGenericRepositoryAsync<Boat> bRepo;

        private string sqlSelect = "select * from Boat";
        private string sqlDelete = "delete from Boat where Boat_Sailnumber = @Boat_Sailnumber";
        #endregion

        #region Properties
        public IEnumerable<Boat> Boats { get; set; }

        [BindProperty]
        public Boat Boat { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterBy { get; set; }

        [BindProperty(SupportsGet =true)]
        public BoatType? SelectedBoatType { get; set; }
        #endregion

        #region Constructor
        public IndexModel(IGenericRepositoryAsync<Boat> boatRepository)
        {
            bRepo = boatRepository;
        }
        #endregion

        #region Methods
        public async Task OnGet()
        {
            try
            {
                Boats = BoatFilter(await bRepo.GetAllObjectsAsync(sqlSelect, BoatReader.Reader));
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
        }

        private IEnumerable<Boat> BoatFilter(IEnumerable<Boat> boats)
        {
            List<Predicate<Boat>> predicates = new List<Predicate<Boat>>();
            if (SelectedBoatType.HasValue)
            {
                predicates.Add(b => b.Boat_TheBoatType == SelectedBoatType.Value);
            }
            if (!string.IsNullOrWhiteSpace(FilterCriteria))
            {
                switch (FilterBy)
                {
                    case "All":
                        predicates.Add(b => b.FilterAll().Contains(FilterCriteria, StringComparison.OrdinalIgnoreCase));
                        break;
                    case "BoatType":
                        predicates.Add(b => !string.IsNullOrEmpty(b.Boat_TheBoatType.ToString()) && b.Boat_TheBoatType.ToString().Contains(FilterCriteria, StringComparison.OrdinalIgnoreCase));
                        break;
                    case "Model":
                        predicates.Add(b => !string.IsNullOrEmpty(b.Boat_Model) && b.Boat_Model.Contains(FilterCriteria, StringComparison.OrdinalIgnoreCase));
                        break;
                    case "Sailnumber":
                        predicates.Add(b => !string.IsNullOrEmpty(b.Boat_Sailnumber) && b.Boat_Sailnumber.Contains(FilterCriteria, StringComparison.OrdinalIgnoreCase));
                        break;
                    case "EngineInfo":
                        predicates.Add(b => !string.IsNullOrEmpty(b.Boat_EngineInfo) && b.Boat_EngineInfo.Contains(FilterCriteria, StringComparison.OrdinalIgnoreCase));
                        break;
                    case "YearOfConstruction":
                        predicates.Add(b => !string.IsNullOrEmpty(b.Boat_yearofconstruction) && b.Boat_yearofconstruction.Contains(FilterCriteria, StringComparison.OrdinalIgnoreCase));
                        break;
                }
            }
            return FilterFunctions<Boat>.Filter(boats, predicates);
        }

        public IActionResult OnPostDelete(string sailNumber)
        {
            bRepo.RemoveObjectAsync(sqlDelete, "Boat_Sailnumber", sailNumber);
            return RedirectToPage("index");
        }
        #endregion
    }
}

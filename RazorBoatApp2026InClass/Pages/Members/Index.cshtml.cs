using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Helpers.Filter;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class IndexModel : PageModel
    {
        #region Instance fields
        private IMemberRepoAsync mRepo;
        #endregion

        #region Properties
        public IEnumerable<Member> Members { get; set; }
        [BindProperty]
        public Member Member { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        [BindProperty(SupportsGet = true)]
        public MemberType? SelectedMemberType { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterBy { get; set; }
        #endregion

        #region Constructor
        public IndexModel(IMemberRepoAsync memberRepository)
        {
            mRepo = memberRepository;
        }
        #endregion

        #region Methods
        public async Task OnGet()
        {
            try
            {
                Members = MemberFilter(await mRepo.GetAllMembers());
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
        }
        public async Task<IActionResult> OnPostDelete()
        {
            try
            {
                await mRepo.RemoveMember(Member);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("index");
        }
        private IEnumerable<Member> MemberFilter(IEnumerable<Member> members)
        {
            List<Predicate<Member>> predicates = new List<Predicate<Member>>();
            if (SelectedMemberType.HasValue)
            {
                predicates.Add(b => b.TheMemberType == SelectedMemberType.Value);
            }
            if (!string.IsNullOrWhiteSpace(FilterCriteria))
            {
                switch (FilterBy)
                {
                    case "All":
                        predicates.Add(b => b.FilterAll().Contains(FilterCriteria, StringComparison.OrdinalIgnoreCase));
                        break;
                    case "FirstName":
                        predicates.Add(b => !string.IsNullOrEmpty(b.FirstName) && b.FirstName.Contains(FilterCriteria, StringComparison.OrdinalIgnoreCase));
                        break;
                    case "SurName":
                        predicates.Add(b => !string.IsNullOrEmpty(b.SurName) && b.SurName.Contains(FilterCriteria, StringComparison.OrdinalIgnoreCase));
                        break;
                    case "PhoneNumber":
                        predicates.Add(b => !string.IsNullOrEmpty(b.PhoneNumber) && b.PhoneNumber.Contains(FilterCriteria, StringComparison.OrdinalIgnoreCase));
                        break;
                    case "Mail":
                        predicates.Add(b => !string.IsNullOrEmpty(b.Mail) && b.Mail.Contains(FilterCriteria, StringComparison.OrdinalIgnoreCase));
                        break;
                    case "City":
                        predicates.Add(b => !string.IsNullOrEmpty(b.City) && b.City.Contains(FilterCriteria, StringComparison.OrdinalIgnoreCase));
                        break;
                    default:
                        break;
                }
            }
            return FilterFunctions<Member>.Filter(members, predicates);
        }
        #endregion
    }
}

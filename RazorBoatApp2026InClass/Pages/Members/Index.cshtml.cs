using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Helpers.Filter;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class IndexModel : PageModel
    {
        private IMemberRepository mRepo;

        public List<Member> Members { get; set; }
        [BindProperty]
        public Member Member { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        [BindProperty(SupportsGet = true)]
        public MemberType? SelectedMemberType { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterBy { get; set; }


        public IndexModel(IMemberRepository memberRepository)
        {
            mRepo = memberRepository;
        }
        public void OnGet()
        {
            Members = MemberFilter(mRepo.GetAllMembers());
        }
        public IActionResult OnPost()
        {
            mRepo.RemoveMember(Member);
            return RedirectToPage("index");
        }
        private List<Member> MemberFilter(List<Member> members)
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
    }
}

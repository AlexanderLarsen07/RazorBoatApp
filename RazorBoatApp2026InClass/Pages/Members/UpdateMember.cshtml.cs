using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class UpdateMemberModel : PageModel
    {
        private IMemberRepository _repo;

        [BindProperty]
        public Member Member { get; set; }
        public UpdateMemberModel(IMemberRepository memberRepository)
        {
            _repo = memberRepository;
        }
        public IActionResult OnGet(string phoneNumber)
        {
            Member = _repo.SearchMember(phoneNumber);
            return Page();
        }
        public IActionResult OnPost()
        {
            _repo.UpdateMember(Member);
            return RedirectToPage("index");
        }
    }
}

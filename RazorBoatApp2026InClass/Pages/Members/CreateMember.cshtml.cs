using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class CreateMemberModel : PageModel
    {
        private IMemberRepository _repo;

        [BindProperty]
        public Member Member { get; set; }
        public CreateMemberModel(IMemberRepository memberRepository)
        {
            _repo = memberRepository;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            _repo.AddMember(Member);
            return RedirectToPage("index");
        }
    }
}

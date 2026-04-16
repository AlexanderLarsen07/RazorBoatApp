using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class UpdateMemberModel : PageModel
    {
        #region Instance fields
        private IMemberRepoAsync _repo;
        #endregion

        #region Properties
        [BindProperty]
        public Member Member { get; set; }
        #endregion

        #region Constructor
        public UpdateMemberModel(IMemberRepoAsync memberRepository)
        {
            _repo = memberRepository;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                Member = await _repo.SearchMember(id);
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            try
            {
                await _repo.UpdateMember(Member);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("index");
        }
        #endregion
    }
}

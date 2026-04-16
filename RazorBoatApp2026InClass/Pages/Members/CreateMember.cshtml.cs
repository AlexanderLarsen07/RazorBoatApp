using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class CreateMemberModel : PageModel
    {
        #region Instance fields
        private IMemberRepoAsync _repo;
        private IWebHostEnvironment webHostEnvironment;
        #endregion

        #region Properties
        [BindProperty]
        public Member Member { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }
        #endregion

        #region Constructor
        public CreateMemberModel(IMemberRepoAsync memberRepository, IWebHostEnvironment webHost)
        {
            _repo = memberRepository;
            webHostEnvironment = webHost;
        }
        #endregion

        #region Methods
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (Photo != null)
            {
                if (Member.MemberImage != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "/images/MemberImages", Member.MemberImage);
                    System.IO.File.Delete(filePath);
                }

                Member.MemberImage = ProcessUploadedFile();
            }
            else
            {
                Member.MemberImage = "Default_Image.png";
            }
            try
            {
                await _repo.AddMember(Member);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("index");
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/MemberImages");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        #endregion
    }
}

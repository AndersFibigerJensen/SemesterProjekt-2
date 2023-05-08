using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.Members
{
    public class AddMemberModel : PageModel
    {
        [BindProperty]
        public Member Member { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        private IMemberService _memberService { get; set; }
        private IWebHostEnvironment _webHostEnvironment { get; set; }

        public AddMemberModel(IMemberService memberService, IWebHostEnvironment Webhostenvironment)
        {
            _memberService = memberService;
            _webHostEnvironment = Webhostenvironment;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult > OnPostAsync() 
        {
            try
            {
                if (Image != null)
                {
                    if (Member.Image != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", Member.Image);
                        System.IO.File.Delete(filePath);
                    }

                    Member.Image = ProcessUploadedFile();
                }
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                _memberService.AddMemberAsync(Member);
                return RedirectToPage("GetAllMembers");
            }
            catch (Exception ex) 
            {
                ViewData["Errormessage"] = ex.Message;
                return Page();

            }

        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}

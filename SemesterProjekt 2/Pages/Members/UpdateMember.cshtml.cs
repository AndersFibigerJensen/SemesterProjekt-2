using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using SemesterProjekt_2.Services;
using System.Diagnostics.Metrics;

namespace SemesterProjekt_2.Pages.Members
{
    public class UpdateMemberModel : PageModel
    {
        [BindProperty]
        public Member Member { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        [BindProperty]
        public Member User { get; set; }

        private IMemberService _memberService { get; set; }
        private IWebHostEnvironment _webHostEnvironment { get; set; }

        public UpdateMemberModel(IMemberService memberService, IWebHostEnvironment webHostEnvironment)
        {
            _memberService = memberService;
            _webHostEnvironment = webHostEnvironment;
        }



        public async Task OnGetAsync(int id)
        {
            try
            {
                string username = HttpContext.Session.GetString("email");
                string password = HttpContext.Session.GetString("password");
                if (username != null)
                {
                    User = await _memberService.LoginMemberAsync(username, password);
                }
                else
                {
                    User = await _memberService.GetMemberByIdAsync(-1);
                }
                Member = await _memberService.GetMemberByIdAsync(id);
            }
            catch (Exception ex) 
            {
                ViewData["Errormessage"] = ex.Message;

            }
            
        }

        public async Task<IActionResult> OnpostAsync(int id)
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
                
                //if (!ModelState.IsValid)
                //{
                //    return Page();
                //}
                if(User.MemberID==Member.MemberID)
                {
                    HttpContext.Session.SetString("email", Member.Email);
                    HttpContext.Session.SetString("password", Member.Password);
                }
                await _memberService.UpdateMemberAsync(id, Member);
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

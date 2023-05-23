using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;

namespace SemesterProjekt_2.Pages.Login
{
    public class LogoutModel : PageModel
    {

        private IMemberService memberService;

        public LogoutModel(IMemberService memberService)
        {
            this.memberService = memberService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnpostAsync()
        {
            try
            {
                HttpContext.Session.Remove("email");
                HttpContext.Session.Remove("password");

            }
            catch (Exception ex) 
            {
                ViewData["Message"] = ex.Message;
                return Page();
            }
            return RedirectToPage("/BlogPosts/GetAllBlog");
        }

    }
}

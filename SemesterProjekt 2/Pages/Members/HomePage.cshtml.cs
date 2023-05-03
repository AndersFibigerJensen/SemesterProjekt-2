using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Models;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Services;

namespace SemesterProjekt_2.Pages.Members
{
    public class HomePageModel : PageModel
    {

        private IMemberService memberService;
        private LoginService loginService;

        [BindProperty]
        public Member member { get; set; }

        public HomePageModel(IMemberService memberService, LoginService loginService)
        {
            this.memberService = memberService;
            this.loginService = loginService;
        }

        public async Task OnGetAsync()
        {
           string email=HttpContext.Session.GetString("email");
            string password=HttpContext.Session.GetString("password");
            if (email!=null & password!=null)
            {
                member= await memberService.LoginMemberAsync(email, password);
            }
        }
    }
}

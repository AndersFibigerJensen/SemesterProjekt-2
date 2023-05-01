using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using SemesterProjekt_2.Services;

namespace SemesterProjekt_2.Pages.Events
{
    public class HomePageModel : PageModel
    {

        private IMemberService memberService;
        private LoginService loginService;

        [BindProperty]
        public Member member { get; set; }

        public HomePageModel(IMemberService MemberService,LoginService loginservice)
        {
            memberService= MemberService;
            this.loginService= loginservice;
        }

        public async Task OnGetAsync()
        {
            if(await loginService.GetLogged()!=null)
            {
                member= await loginService.GetLogged();
            }
            else
            {
                member = await memberService.GetMemberByIdAsync(-1);
            }
        }
    }
}

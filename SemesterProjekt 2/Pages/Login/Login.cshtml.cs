using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Models;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Services;

namespace SemesterProjekt_2.Pages.Login
{
    public class LoginModel : PageModel
    {
        private IMemberService _memberService;
        private LoginService _loginService;

        [BindProperty]
        public Member member { get; set; }

        public LoginModel(IMemberService memberService, LoginService loginService)
        {
             _memberService= memberService;
            _loginService = loginService;
        }

        public async Task OnGetAsync()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            List<Member> members = await _memberService.GetAllMembersAsync();
            foreach(Member user in members)
            {
                if(user.Email==member.Email & user.Password==member.Password)
                {
                    HttpContext.Session.SetString("email",user.Email);
                    HttpContext.Session.SetString("password",user.Password);

                    return RedirectToPage("/Members/HomePage");
                }

            }
            return Page();
        }
    }
}

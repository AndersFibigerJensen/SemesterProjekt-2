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
        public Member User { get; set; }

        private IMemberService _memberService { get; set; }
        private LoginService _loginService { get; set; }

        public UpdateMemberModel(IMemberService memberService,LoginService LoginService)
        {
            _memberService = memberService;
            _loginService = LoginService;
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
                if(!ModelState.IsValid)
                {
                    return Page();
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
    }
}

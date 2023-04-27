using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using SemesterProjekt_2.Services;

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
                User = await _loginService.GetLogged();
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

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

        private IMemberService _memberService { get; set; }

        public AddMemberModel(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult > OnPostAsync() 
        {
            try
            {
                if(!ModelState.IsValid)
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
    }
}

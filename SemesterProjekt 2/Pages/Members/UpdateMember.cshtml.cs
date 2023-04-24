using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.Members
{
    public class UpdateMemberModel : PageModel
    {
        [BindProperty]
        public Member Member { get; set; }

        private IMemberService _memberService { get; set; }

        public UpdateMemberModel(IMemberService memberService)
        {
            _memberService = memberService;
        }



        public async Task OnGetAsync(int id)
        {
            Member= await _memberService.GetMemberByIdAsync(id);
        }

        public async Task<IActionResult> OnpostAsync(int id)
        {
            await _memberService.UpdateMemberAsync(id,Member);
            return RedirectToPage("GetAllMembers");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.Members
{
    public class DeleteMemberModel : PageModel
    {
        [BindProperty]
        public Member Member { get; set; }

        private IMemberService _memberService { get; set; }

        public DeleteMemberModel(IMemberService memberService)
        {
            _memberService= memberService;
        }

        public async Task OnGetAsync(int id)
        {
            Member= await _memberService.GetMemberByIdAsync(id);
        }

        public async Task<IActionResult> OnpostAsync()
        {
            await _memberService.DeleteMemberAsync(Member.MemberID);
            return RedirectToPage("GetAllMembers");
        }
    }
}

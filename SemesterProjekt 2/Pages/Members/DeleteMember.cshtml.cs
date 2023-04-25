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
            try
            {
                Member = await _memberService.GetMemberByIdAsync(id);
            }
            catch (Exception ex) 
            {
                ViewData["Errormessage"] = ex.Message;

            }
            
        }

        public async Task<IActionResult> OnpostAsync()
        {
            try
            {

            }
            catch (Exception ex) 
            {
                ViewData["Errormessage"] = ex.Message;

            }
            await _memberService.DeleteMemberAsync(Member.MemberID);
            return RedirectToPage("GetAllMembers");
        }
    }
}

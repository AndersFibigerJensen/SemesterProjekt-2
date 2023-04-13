using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.Medlemmer
{
    public class GetAllMembersModel : PageModel
    {

        public List<Member> Members { get; set; }

        private IMemberService _memberService;

        public GetAllMembersModel(IMemberService memberservice)
        {
            memberservice = _memberService;
        }

        public async Task OnGetAsync()
        {
            Members=await _memberService.GetAllMembersAsync();
        }
    }
}

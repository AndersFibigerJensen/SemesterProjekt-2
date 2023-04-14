using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.Members
{
    public class GetAllMembersModel : PageModel
    {

        public List<Member> Members { get; set; }

        private IMemberService _memberService { get; set; }

        public GetAllMembersModel(IMemberService memberService)
        {
            _memberService=memberService;
        }

        public async Task OnGetAsync()
        {
            Members= await _memberService.GetAllMembersAsync();
        }
    }
}

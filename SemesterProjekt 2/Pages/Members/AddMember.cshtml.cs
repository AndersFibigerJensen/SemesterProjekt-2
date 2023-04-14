using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.Members
{
    public class AddMemberModel : PageModel
    {

        public Member Member { get; set; }

        private IMemberService _memberService { get; set; }

        public AddMemberModel(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public void OnGet()
        {
        }

        public async Task OnPostAsync() 
        {
            _memberService.AddMemberAsync(Member);
        }
    }
}

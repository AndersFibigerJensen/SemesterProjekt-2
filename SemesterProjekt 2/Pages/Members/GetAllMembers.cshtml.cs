using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.Members
{
    public class GetAllMembersModel : PageModel
    {

        public List<Member> Members { get; set; }

        public Member Member { get; set; }

        private IMemberService _memberService { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public GetAllMembersModel(IMemberService memberService)
        {
            _memberService=memberService;
        }

        public async Task OnGetAsync()
        {
            try
            {
                if (!FilterCriteria.IsNullOrEmpty())
                {
                    Members = await _memberService.FilterMembersAsync(FilterCriteria);
                }
                else
                {
                    Members = await _memberService.GetAllMembersAsync();
                }

            }
            catch (Exception ex) 
            {
                ViewData["Errormessage"] = ex.Message;

            }
           
           
        }
    }
}

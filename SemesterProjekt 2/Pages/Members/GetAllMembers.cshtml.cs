using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using SemesterProjekt_2.Services;

namespace SemesterProjekt_2.Pages.Members
{
    public class GetAllMembersModel : PageModel
    {

        public List<Member> Members { get; set; }

        [BindProperty]
        public Member member { get; set; }

        private IMemberService _memberService { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public GetAllMembersModel(IMemberService memberService)
        {
            _memberService=memberService;
        }

        public async Task OnGetAsync()
        {
            string username=HttpContext.Session.GetString("email");
            string password=HttpContext.Session.GetString("password");
            if (username!=null)
            {
                member = await _memberService.LoginMemberAsync(username, password);
            }
            else
            {
                member = await _memberService.GetMemberByIdAsync(-1);
            }
            
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

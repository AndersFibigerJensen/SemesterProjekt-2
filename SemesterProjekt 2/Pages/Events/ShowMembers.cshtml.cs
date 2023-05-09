using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using SemesterProjekt_2.Services;
using System.Diagnostics.Metrics;


namespace SemesterProjekt_2.Pages.Events
{
    public class ShowMembersModel : PageModel
    {
        public List<int> Members { get; set; }
        public Member Member { get; set; }
        private IEventService _eservice { get; set; }
        private IMemberService _memberService { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        public ShowMembersModel(IEventService eservice, IMemberService memberService)
        {
            _eservice = eservice;
            _memberService = memberService;
        }

        public async Task OnGetAsync(int eventid)
        {
            Members = await _eservice.ReturnMembers(eventid);

        }
    }
}

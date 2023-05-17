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
        [BindProperty]
        public List<int> Members { get; set; }

        public List<Member> Truemembers { get; set; }
        [BindProperty]
        public Member Member { get; set; }
        [BindProperty]
        public Event Event { get; set; }
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
            Event = await _eservice.GetEventByIdAsync(eventid);
            string email = HttpContext.Session.GetString("email");
            string password = HttpContext.Session.GetString("password");
            if (email != null & password != null)
            {
                Member = await _memberService.LoginMemberAsync(email, password);
            }
            else
            {
                Member = await _memberService.GetMemberByIdAsync(-1);
            }
            //if (!FilterCriteria.IsNullOrEmpty())
            //{
            //    foreach(int id in Members)
            //    {
            //         await _memberService.GetMemberByIdAsync(id);
            //        Truemembers.
            //    }

            //}


        }
    }
}

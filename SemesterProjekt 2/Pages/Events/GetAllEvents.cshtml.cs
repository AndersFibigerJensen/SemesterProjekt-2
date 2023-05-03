using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Services;
using SemesterProjekt_2.Models;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Metrics;

namespace SemesterProjekt_2.Pages.Events
{
    public class GetAllEventsModel : PageModel
    {

        public List<Event> Events { get; set; }

        public Event Event { get; set; }

        private IEventService _eService { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        public Member user { get; set; }
        private IMemberService _memberService { get; set; }

        public GetAllEventsModel(IEventService eventService, IMemberService memberService)
        {
            _eService = eventService;
            _memberService = memberService;
        }

        public async Task OnGetAsync()
        {
            string email = HttpContext.Session.GetString("email");
            string password = HttpContext.Session.GetString("password");
            if (email != null & password != null)
            {
                user = await _memberService.LoginMemberAsync(email, password);
            }
            else
            {
                user = await _memberService.GetMemberByIdAsync(-1);
            }
            if (!FilterCriteria.IsNullOrEmpty())
            {
                Events = await _eService.FilterEventsAsync(FilterCriteria);
            }

            else

            {
                Events = await _eService.GetAllEventsAsync();
            }
            


        }
    }
}

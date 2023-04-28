using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Services;
using SemesterProjekt_2.Models;
using Microsoft.IdentityModel.Tokens;

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
        private LoginService _loginService { get; set; }
        private IMemberService _memberService { get; set; }

        public GetAllEventsModel(IEventService eventService, LoginService loginService, IMemberService memberService)
        {
            _eService = eventService;
            _loginService = loginService;
            _memberService = memberService;
        }

        public async Task OnGetAsync()
        {
            if (await _loginService.GetLogged() != null)
            {
                user = await _loginService.GetLogged();
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

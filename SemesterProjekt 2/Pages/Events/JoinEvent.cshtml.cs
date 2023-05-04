using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using SemesterProjekt_2.Services;

namespace SemesterProjekt_2.Pages.Events
{
    public class JoinEventModel : PageModel
    {
        [BindProperty]
        public Event Event { get; set; }
        
        [BindProperty]
        public Member member { get; set; }

        private IEventService _eService { get; set; }

        private IMemberService _memberService;

        public JoinEventModel(IEventService eService, IMemberService memberService)
        {
            _eService = eService;
            _memberService = memberService;
        }

        public async Task OnGetAsync(int eventID)
        {

            try
            {
                string email = HttpContext.Session.GetString("email");
                string password = HttpContext.Session.GetString("password");
                if (email != null & password != null)
                {
                    member = await _memberService.LoginMemberAsync(email, password);
                }
                Event = await _eService.GetEventByIdAsync(eventID);
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
            await _eService.JoinEvent(Event.eventID, member.MemberID);
            return RedirectToPage("GetAllEvents");
        }
    }
}

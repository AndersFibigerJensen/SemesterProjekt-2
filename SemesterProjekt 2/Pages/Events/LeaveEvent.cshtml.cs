using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.Events
{
    public class LeaveEventModel : PageModel
    {
        [BindProperty]
        public Event Event { get; set; }

        [BindProperty]
        public Member user { get; set; }

        [BindProperty]
        public Member member { get; set; }

        private IEventService _eService { get; set; }

        private IMemberService _memberService;

        public LeaveEventModel(IEventService eService, IMemberService memberService)
        {
            _eService = eService;
            _memberService = memberService;
        }

        public async Task OnGetAsync(int eventID ,int itemid)
        {

            try
            {
                string email = HttpContext.Session.GetString("email");
                string password = HttpContext.Session.GetString("password");
                if (email != null & password != null)
                {
                    user = await _memberService.LoginMemberAsync(email, password);
                }
                Event = await _eService.GetEventByIdAsync(eventID);
                if(itemid !=0)
                {
                    user= await _memberService.GetMemberByIdAsync(itemid);
                }
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
            await _eService.DeleteEventMember(Event.eventID, user.MemberID);
            return RedirectToPage("GetAllEvents");
        }
    }
}

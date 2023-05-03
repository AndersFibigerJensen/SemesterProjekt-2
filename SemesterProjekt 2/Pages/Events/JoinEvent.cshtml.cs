using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.Events
{
    public class JoinEventModel : PageModel
    {
        [BindProperty]
        public Event Event { get; set; }
        public Member Member { get; set; }

        private IEventService _eService { get; set; }

        public JoinEventModel(IEventService eService)
        {
            _eService = eService;
        }

        public async Task OnGetAsync(int eventID)
        {
            try
            {
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
            await _eService.JoinEvent(Event.eventID, Member.MemberID);
            return RedirectToPage("GetAllEvents");
        }
    }
}

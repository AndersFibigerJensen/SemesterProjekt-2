using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.Events
{
    public class UpdateEventModel : PageModel
    {
        [BindProperty]
        public Event Event { get; set; }

        private IEventService _eService { get; set; }

        public UpdateEventModel(IEventService eService)
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

        public async Task<IActionResult> OnpostAsync(int eventID)
        {
            try
            {
                await _eService.UpdateEventAsync(Event, eventID);
                return RedirectToPage("GetAllEvents");
            }
            catch (Exception ex)
            {
                ViewData["Errormessage"] = ex.Message;
                return Page();

            }

        }
    }
}

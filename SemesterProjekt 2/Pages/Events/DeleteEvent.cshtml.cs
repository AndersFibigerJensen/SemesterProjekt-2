using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.Events
{
    public class DeleteEventModel : PageModel
    {
        [BindProperty]
        public Event Event { get; set; }

        private IEventService _eService { get; set; }

        public DeleteEventModel(IEventService eService)
        {
            _eService = eService;
        }

        public async Task OnGetAsync(int id)
        {
            try
            {
                Event = await _eService.GetEventByIdAsync(id);
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
            await _eService.RemoveEventAsync(Event.eventID);
            return RedirectToPage("GetAllEvents");
        }
    }
}

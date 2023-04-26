using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Models;
using SemesterProjekt_2.Interfaces;

namespace SemesterProjekt_2.Pages.Events
{
    public class AddEventModel : PageModel
    {
        [BindProperty]
        public Event Event { get; set; }

        private IEventService _eservice;
        public AddEventModel(IEventService eventService)
        {
            _eservice = eventService;
        }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _eservice.AddEventAsync(Event);
                return RedirectToPage("GetAllEvents", Event);
            }
            catch (Exception ex)
            {
                ViewData["Errormessage"] = ex.Message;
                return Page();
            }
            
        }
    }
}

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
        private IShiftService sService;

        public DeleteEventModel(IEventService eService, IShiftService shiftService)
        {
            _eService = eService;
            sService = shiftService;
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

        public async Task<IActionResult> OnpostAsync(int eventid)
        {
            try
            {
                //Luna
                List<Shift> assignedShifts = await sService.GetAllShiftsByEventIdAsync(eventid);

                foreach (Shift s in assignedShifts)
                {
                    Shift newS = new Shift(s.ShiftID, s.DateFrom, s.DateTo, s.MemberID, null, s.ShiftType);
                    await sService.UpdateEventIDAsync(newS, s.ShiftID);
                }
                //!Luna
                await _eService.RemoveEventAsync(Event.eventID);
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

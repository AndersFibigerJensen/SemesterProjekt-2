using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.Shifts
{
    public class ShiftEventModel : PageModel
    {
        [BindProperty]
        public List<Event> Events { get; set; }
        [BindProperty]
        public int ShiftID { get; set; }

        private IEventService eService { get; set; }
        private IShiftService sService { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public ShiftEventModel(IShiftService shiftService, IEventService eventService)
        {
            sService = shiftService;
            eService = eventService;
        }

        public async Task OnGetAsync(int shiftid)
        {
            ShiftID = shiftid;

            if (!FilterCriteria.IsNullOrEmpty())
            {
                Events = await eService.FilterEventsAsync(FilterCriteria);
            }
            else
            {
                Events = await eService.GetAllEventsAsync();
            }
        }

        public async Task<IActionResult> OnPostAsync(int shiftid, int? eventID)
        {
            Event currentEvent = new Event();
            int? newEventID = eventID;

            if (eventID < 0)
                newEventID = null;
            else if (eventID != null)
                currentEvent = await eService.GetEventByIdAsync(eventID ?? 0);

            Shift TBU = await sService.GetShiftByIdAsync(shiftid);
            Shift newShift = new Shift(TBU.ShiftID, TBU.DateFrom, TBU.DateTo, TBU.MemberID, newEventID, TBU.ShiftType);

            var hoursFrom = (currentEvent.eventStart - TBU.DateFrom).TotalHours;
            var hoursTo = (currentEvent.eventEnd - TBU.DateTo).TotalHours;

            if (hoursFrom < 0)
                hoursFrom = hoursFrom * (-1);
            if (hoursTo < 0)
                hoursTo = hoursTo * (-1);

            try
            {
                if (hoursFrom > 24)
                    throw new Exception("Event and shift startpoints are above 24 hours apart.");
                if (hoursTo > 24)
                    throw new Exception("Event and shift endpoints are above 24 hours apart.");

                await sService.UpdateEventIDAsync(newShift, TBU.ShiftID);

                return RedirectToPage("GetAllShifts");
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                Events= await eService.GetAllEventsAsync();
                return Page();
            }
        }
    }
}

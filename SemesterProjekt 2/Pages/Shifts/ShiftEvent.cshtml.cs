using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.Shifts
{
    public class ShiftEventModel : PageModel
    {
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
            Shift TBU = await sService.GetShiftByIdAsync(shiftid);
            Shift newShift = new Shift(TBU.ShiftID, TBU.DateFrom, TBU.DateTo, TBU.MemberID, eventID);

            await sService.UpdateEventIDAsync(newShift, TBU.ShiftID);

            return RedirectToPage("GetAllShifts");
        }
    }
}

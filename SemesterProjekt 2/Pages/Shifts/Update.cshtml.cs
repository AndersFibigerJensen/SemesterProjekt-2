using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.Shifts
{
    public class UpdateModel : PageModel
    {
        private IShiftService sService;
        [BindProperty]
        public Shift TBU { get; set; }
        [BindProperty]
        public Shift NewShift { get; set; }

        public UpdateModel(IShiftService shiftService)
        {
            sService = shiftService;
        }

        public async Task OnGet(int shiftid)
        {
            TBU = await sService.GetShiftByIdAsync(shiftid);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                DateTime shiftStart = NewShift.DateFrom;
                DateTime shiftEnd = NewShift.DateTo;
                DateTime timeNow = DateTime.Now;
                if (shiftStart <= timeNow)
                {
                    throw new ArgumentOutOfRangeException("Error. Check your variables, shift cannot start in the past.");
                }
                if (shiftStart >= shiftEnd)
                {
                    throw new ArgumentOutOfRangeException("Error. Check your variables, shift must begin before it can end.");
                }

                await sService.UpdateShiftAsync(NewShift, TBU.ShiftID);

                return RedirectToPage("GetAllShifts");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }
        }
    }
}

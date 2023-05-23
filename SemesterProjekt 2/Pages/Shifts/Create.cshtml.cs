using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.Shifts
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Shift Shift { get; set; }
        private IShiftService sService;

        public CreateModel(IShiftService shiftService)
        {
            sService = shiftService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                //if ((int)Shift.ShiftType > 5 || (int)Shift.ShiftType < 1)
                //    throw new Exception("Please select a shift type.");
                DateTime shiftStart = Shift.DateFrom;
                DateTime shiftEnd = Shift.DateTo;
                DateTime timeNow = DateTime.Now;
                if (shiftStart <= timeNow)
                {
                    throw new ArgumentOutOfRangeException("Check your variables, shift cannot start in the past.");
                }
                if (shiftStart >= shiftEnd)
                {
                    throw new ArgumentOutOfRangeException("Check your variables, shift must begin before it can end.");
                }


                await sService.AddShiftAsync(Shift);
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

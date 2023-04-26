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
            await sService.AddShiftAsync(Shift);

            return RedirectToPage("GetAllShifts");
        }
    }
}

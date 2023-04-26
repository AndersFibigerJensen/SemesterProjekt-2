using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.Shifts
{
    public class DeleteModel : PageModel
    {
        private IShiftService sService;
        [BindProperty]
        public Shift TBD { get; set; }

        public DeleteModel(IShiftService shiftService)
        {
            sService = shiftService;
        }

        public async Task OnGetAsync(int shiftid)
        {
            TBD = await sService.GetShiftByIdAsync(shiftid);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await sService.DeleteShiftAsync(TBD.ShiftID);

            return RedirectToPage("GetAllHotels");
        }
    }
}

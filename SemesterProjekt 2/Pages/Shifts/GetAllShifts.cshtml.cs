using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.Shifts
{
    public class GetAllShiftsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public DateTime FilterCriteria { get; set; }

        public List<Shift> Shifts { get; set; }

        private IShiftService sService;

        public GetAllShiftsModel(IShiftService shiftService)
        {
            sService = shiftService;
        }

        public async Task OnGetAsync()
        {
            //if (!(FilterCriteria.ToString()).IsNullOrEmpty())
            //{
            //    Shifts = await sService.FilterShiftsAsync(FilterCriteria);
            //}
            //else
            //{
            //    Shifts = await sService.GetAllShiftsAsync();
            //}
            
            Shifts = await sService.GetAllShiftsAsync();
        }
    }
}

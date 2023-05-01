using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using SemesterProjekt_2.Services;

namespace SemesterProjekt_2.Pages.Shifts
{
    public class ShiftMemberModel : PageModel
    {
        public List<Member> Members { get; set; }
        [BindProperty]
        public int ShiftID { get; set; }

        private IMemberService mService { get; set; }
        private IShiftService sService { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public ShiftMemberModel(IShiftService shiftService, IMemberService memberService)
        {
            sService = shiftService;
            mService = memberService;
        }

        public async Task OnGetAsync(int shiftid)
        {
            ShiftID = shiftid;

            if (!FilterCriteria.IsNullOrEmpty())
            {
                Members = await mService.FilterMembersAsync(FilterCriteria);
            }
            else
            {
                Members = await mService.GetAllMembersAsync();
            }
        }

        public async Task<IActionResult> OnPostAsync(int shiftid, int memberID)
        {
            Shift TBU = await sService.GetShiftByIdAsync(shiftid);
            Shift newShift = new Shift(TBU.ShiftID, TBU.DateFrom, TBU.DateTo, memberID, TBU.EventID);

            await sService.UpdateShiftAsync(newShift, TBU.ShiftID);

            return RedirectToPage("GetAllShifts");
        }
    }
}

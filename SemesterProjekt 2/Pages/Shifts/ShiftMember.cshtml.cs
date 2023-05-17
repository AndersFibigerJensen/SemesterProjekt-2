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
            Member hygCheck = await mService.GetMemberByIdAsync(memberID);
            try
            {
                if (hygCheck.HasDoneHygieneCourse == false && memberID != -1)
                {
                    throw new Exception("This member has not done the hygiene course yet. Therefore, they cannot be assigned this shift.");
                }
                else
                {
                    Shift TBU = await sService.GetShiftByIdAsync(shiftid);

                    int? newMemberID = memberID;
                    if (memberID < 0)
                        newMemberID = null;


                    Shift newShift = new Shift(TBU.ShiftID, TBU.DateFrom, TBU.DateTo, newMemberID, TBU.EventID, TBU.ShiftType);

                    await sService.UpdateMemberIDAsync(newShift, TBU.ShiftID);

                    return RedirectToPage("GetAllShifts");
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;

            }

            return RedirectToPage("GetAllShifts");
        }
    }
}

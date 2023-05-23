using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.Members
{
    public class DeleteMemberModel : PageModel
    {
        [BindProperty]
        public Member Member { get; set; }

        private IMemberService _memberService { get; set; }
        private IShiftService sService { get; set; }

        public DeleteMemberModel(IMemberService memberService, IShiftService shiftService)
        {
            _memberService= memberService;
            sService = shiftService;
        }

        public async Task OnGetAsync(int id)
        {
            try
            {
                Member = await _memberService.GetMemberByIdAsync(id);
            }
            catch (Exception ex) 
            {
                ViewData["Errormessage"] = ex.Message;

            }
            
        }

        public async Task<IActionResult> OnpostAsync(int memberid)
        {
            try
            {
                //Luna
                List<Shift> assignedShifts = await sService.GetAllShiftsByMemberIdAsync(memberid);

                foreach(Shift s in assignedShifts)
                {
                    Shift newS = new Shift(s.ShiftID, s.DateFrom, s.DateTo, null, s.EventID, s.ShiftType);
                    await sService.UpdateMemberIDAsync(newS, s.ShiftID);
                }
                //!Luna
                await _memberService.DeleteMemberAsync(Member.MemberID);
                return RedirectToPage("GetAllMembers");

            }
            catch (Exception ex) 
            {
                ViewData["Errormessage"] = ex.Message;
                return Page();

            }

        }
    }
}

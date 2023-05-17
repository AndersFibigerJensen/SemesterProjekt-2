using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using SemesterProjekt_2.Services;
using System.Diagnostics.Metrics;

namespace SemesterProjekt_2.Pages.Shifts
{
    public class GetAllShiftsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public DateTime FilterCriteria { get; set; }

        [BindProperty]
        public Member CLIMember { get; set; }

        public List<Shift> Shifts { get; set; }

        public IMemberService mService;
        private IShiftService sService;
        public LoginService login;

        public GetAllShiftsModel(IShiftService shiftService, IMemberService memberService, LoginService l)
        {
            mService = memberService;
            sService = shiftService;
            login = l;
        }

        public async Task OnGetAsync()
        {
            string username = HttpContext.Session.GetString("email");
            string password = HttpContext.Session.GetString("password");
            if (username != null)
            {
                CLIMember = await mService.LoginMemberAsync(username, password);
            }
            else
            {
                CLIMember = await mService.GetMemberByIdAsync(-1);
            }

            try
            {
                if (FilterCriteria.ToString().IsNullOrEmpty() || FilterCriteria.Year == 1)
                {
                    Shifts = await sService.GetAllShiftsAsync();
                }
                else
                {
                    Shifts = await sService.FilterShiftsAsync(FilterCriteria);
                }

            }
            catch (Exception ex)
            {
                ViewData["Errormessage"] = ex.Message;

            }
        }
    }
}

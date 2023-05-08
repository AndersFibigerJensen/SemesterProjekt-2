using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using SemesterProjekt_2.Services;


namespace SemesterProjekt_2.Pages.Events
{
    public class ShowMembersModel : PageModel
    {
        public List<Member> Members { get; set; }
        public Member Member { get; set; }
        private IEventService _eservice { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        public void OnGet()
        {

        }
    }
}

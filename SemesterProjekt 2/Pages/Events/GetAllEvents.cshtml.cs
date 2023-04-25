using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Services;
using SemesterProjekt_2.Models;
using Microsoft.IdentityModel.Tokens;

namespace SemesterProjekt_2.Pages.Events
{
    public class GetAllEventsModel : PageModel
    {

        public List<Event> Events { get; set; }

        public Event Event { get; set; }

        private IEventService _eService { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public GetAllEventsModel(IEventService eventService)
        {
            _eService = eventService;
        }

        public async Task OnGetAsync()
        {
            //if (!FilterCriteria.IsNullOrEmpty())
            //{
            //    Events = await _eService.FilterEventsAsync(FilterCriteria);
            //}
            //else
            
            {
                Events = await _eService.GetAllEventsAsync();
            }
            


        }
    }
}

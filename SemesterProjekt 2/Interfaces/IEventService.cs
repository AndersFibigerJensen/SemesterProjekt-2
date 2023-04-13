using SemesterProjekt_2.Models;
namespace SemesterProjekt_2.Interfaces
{
    //Adam
    public interface IEventService
    {
        public Task<List<Event>> GetAllEventsAsync();
        public Task AddEventAsync();
        public Task RemoveEventAsync(int id);
        public Task UpdateEventAsync(int id);
        public Task<List<Event>> FilterEventsAsync();
        public Task<Event> GetEventByIdAsync(int id);
    }
}

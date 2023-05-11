using SemesterProjekt_2.Models;
namespace SemesterProjekt_2.Interfaces
{
    //Adam
    public interface IEventService
    {
        /// <summary>
        /// Henter alle events i databasen
        /// </summary>
        /// <returns>events</returns>
        public Task<List<Event>> GetAllEventsAsync();
        /// <summary>
        /// Tilføjer en event til databasen
        /// </summary>
        /// <param name="begivenhed"></param>
        /// <returns></returns>
        public Task AddEventAsync(Event begivenhed);
        /// <summary>
        /// Sletter en event fra databasen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Event> RemoveEventAsync(int id);
        /// <summary>
        /// Opdaterer en event i databasen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> UpdateEventAsync(Event eEvent, int id);
        public Task<List<Event>> FilterEventsAsync(string filter);
        /// <summary>
        /// Finder en event med et givent id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>id</returns>
        public Task<Event> GetEventByIdAsync(int id);
        public Task JoinEvent(int eventid, int memberid);

        public Task<List<int>> ReturnMembers(int eventid);

        public Task<int> CountMembers(int eventid);

        public Task<bool> DeleteEventMember(int eventid,int Memberid);
    }
}

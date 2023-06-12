using SemesterProjekt_2.Models;
namespace SemesterProjekt_2.Interfaces
{
    //Adam
    public interface IEventService
    {
        /// <summary>
        /// Henter alle events i databasen
        /// </summary>
        /// <returns>Alle events</returns>
        public Task<List<Event>> GetAllEventsAsync();
        /// <summary>
        /// Tilføjer en event til databasen
        /// </summary>
        /// <param name="begivenhed">Tilføjer en event inde i parametret til databasen</param>
        /// <returns></returns>
        public Task AddEventAsync(Event begivenhed);
        /// <summary>
        /// Sletter en event fra databasen
        /// </summary>
        /// <param name="id">Sletter en event inde i parametret fra databasen</param>
        /// <returns></returns>
        public Task<Event> RemoveEventAsync(int id);
        /// <summary>
        /// Opdaterer en event i databasen
        /// </summary>
        /// <param name="id">Opdaterer en events properties inde i parametret fra databasen</param>
        /// <returns></returns>
        public Task<bool> UpdateEventAsync(Event eEvent, int id);
        /// <summary>
        /// Søger på efter events i databasen ud fra deres navn og viser dem, der matcher søgningen
        /// </summary>
        /// <param name="filter">Finder en eller flere events i databasen</param>
        /// <returns>event eller events, hvis navn man har søgt efter</returns>
        public Task<List<Event>> FilterEventsAsync(string filter);
        /// <summary>
        /// Finder en event med et givent id
        /// </summary>
        /// <param name="id">Finder en event der matcher parametres id </param>
        /// <returns>id for den angivne event</returns>
        public Task<Event> GetEventByIdAsync(int id);
        /// <summary>
        /// Tilmelder et medlem til en event
        /// </summary>
        /// <param name="eventid">Finder en event i databasen</param>
        /// <param name="memberid">Finder et medlem i databasen</param>
        /// <returns></returns>
        public Task JoinEvent(int eventid, int memberid);
        /// <summary>
        /// Viser alle medlemmer, der har meldt sig til en given event
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns>Alle medlemmer, der har tilmeldt sig den angivne event</returns>
        public Task<List<int>> ReturnMembers(int eventid);
        /// <summary>
        /// Tæller hvor mange medlemmer, der har meldt sig til en given event
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        public Task<int> CountMembers(int eventid);
        /// <summary>
        /// Sletter en tilmelding fra databasen, så et member ikke længere er tilmeldt en event.
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="Memberid"></param>
        /// <returns></returns>
        public Task<bool> DeleteEventMember(int eventid, int Memberid);
        
    }
}

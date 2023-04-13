using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Services
{
    //Adam
    public class EventService : IEventService
    {
        //Query String
        private string queryGetAll = "select * from event";
        private string queryInsert = "insert into event values(@EventID, @Name, @DateFrom, @DateTo, @Price, @IsMemberRequired)";
        private string queryGetFromID = "select * from event where eventId = @EventID";
        private string queryDelete = "delete * from event where eventId = @EventID";
        private string queryUpdate = "update * from event where eventId = @EventID";
        private string querySearch = " select * from event where Name Like '%'+@Name+'%'";
        public Task AddEventAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Event>> FilterEventsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Event>> GetAllEventsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetEventByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveEventAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEventAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

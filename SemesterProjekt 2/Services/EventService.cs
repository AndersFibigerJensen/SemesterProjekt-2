using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using Microsoft.Data.SqlClient;

namespace SemesterProjekt_2.Services
{
    //Adam
    public class EventService :Connection, IEventService
    {
        //Query String
        private string queryGetAll = "select * from event";
        private string queryInsert = "insert into event values(@EventID, @Name, @DateFrom, @DateTo, @Price, @IsMemberRequired)";
        private string queryGetFromID = "select * from event where eventId = @EventID";
        private string queryDelete = "delete * from event where eventId = @EventID";
        private string queryUpdate = "update event Set ID = @EventID, Name = @Name , " +
            "Event Start = @EventStart, Event End = @EventEnd, Price = @Price, Is member required = @IsMemberRequired, where ID = @EventID";
        private string querySearch = " select * from event where Name Like '%'+@Name+'%'";

        public EventService(IConfiguration configuration) : base(configuration)
        {
        }

        public Task AddEventAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Event>> FilterEventsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            List<Event> events = new List<Event>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryGetAll, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@EventID", events);
                        await command.Connection.OpenAsync();

                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            DateTime start = reader.GetDateTime(2);
                            DateTime end = reader.GetDateTime(3);
                            double price = reader.GetDouble(4);
                            bool isMemberRequired = reader.GetBoolean(5);
                            Event begivenheder = new (id, name, start, end, price, isMemberRequired);
                            events.Add(begivenheder);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel ejl " + ex.Message);
                    }
                    finally
                    {

                    }
                }
            }
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

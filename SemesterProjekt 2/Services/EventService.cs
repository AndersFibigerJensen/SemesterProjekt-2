using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Windows.Input;

namespace SemesterProjekt_2.Services
{
    //Adam
    public class EventService :Connection, IEventService
    {
        //Query String
        private string queryGetAll = "select * from Event";
        private string queryInsert = "insert into Event values(@Name, @DateFrom, @DateTo, @Price, @IsMemberRequired, @Capacity)";
        private string queryGetFromID = "select * from Event where eventid = @EventID";
        private string queryDelete = "delete from Event where eventid = @EventID";
        private string queryUpdate = "update Event Set Name = @Name , " +
            "EventStart = @EventStart, EventEnd = @EventEnd, Price = @Price, IsMemberRequired = @IsMemberRequired, Capacity = @Capacity where eventid = @EventID";
        private string querySearch = " select * from event where name Like '%'+@Name+'%'";
        private string queryJoin = "insert into EventMember(eventid, MemberID) values(@EventID, @MemberID)";
        private string queryGetMembers = "select * from EventMember where eventid = @EventID";
        private string CountingMembers = "select Count(MemberID) from EventMember where eventid=@EventID";
        private string quaryDeleteEventMember = "delete from Event where eventid = @EventID and MemberID=@MemberID";

        public EventService(IConfiguration configuration) : base(configuration)
        {

        }
        public EventService(string connectionstring) :base(connectionstring)
        {

        }
        /// <summary>
        /// Tilføjer en event til databasen
        /// </summary>
        /// <param name="begivenhed"></param>
        /// /// <exception cref="ex"> exceptionen bliver kastet videre i systemet</exception>
        /// <exception cref="sql"> exceptionen bliver kastet videre i systemet</exception>
        /// <returns></returns>
        public async Task AddEventAsync(Event begivenhed)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryInsert, connection))
                {
                    
                    command.Parameters.AddWithValue("@Name", begivenhed.name);
                    command.Parameters.AddWithValue("@DateFrom", begivenhed.eventStart);
                    command.Parameters.AddWithValue("@DateTo", begivenhed.eventEnd);
                    command.Parameters.AddWithValue("@Price", begivenhed.price);
                    command.Parameters.AddWithValue("@IsMemberRequired", begivenhed.isMemberRequired);
                    command.Parameters.AddWithValue("@Capacity", begivenhed.capacity);
                    command.Connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    try
                    {
                        
                    }

                    catch (SqlException sql)
                    {
                        throw sql;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        /// <summary>
        /// Søger efter events i databasen, hvis navn passer til søgningen og viser dem til brugeren.
        /// </summary>
        /// <param name="filter"></param>
        /// <exception cref="ex"> exceptionen bliver kastet videre i systemet</exception>
        /// <exception cref="sql"> exceptionen bliver kastet videre i systemet</exception>
        /// <returns>events, med navne, der passer til søgningen</returns>

        public async Task<List<Event>> FilterEventsAsync(string filter)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(querySearch, connection))
                {
                    try
                    {
                        List<Event> events = new List<Event>();
                        command.Parameters.AddWithValue("@Name", filter);

                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int eventId = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            DateTime eventStart = reader.GetDateTime(2);
                            DateTime eventEnd = reader.GetDateTime(3);
                            double price = reader.GetDouble(4);
                            bool isMemberRequired = reader.GetBoolean(5);
                            int capacity = reader.GetInt32(6);
                            
                            Event eEvent = new Event(eventId, name, eventStart, eventEnd, price, isMemberRequired, capacity);
                            events.Add(eEvent);
                        }
                        return events;
                    }
                    catch (SqlException sqlEx)
                    {
                        throw sqlEx;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
            return null;
        }
        /// <summary>
        /// Viser alle events i databasen
        /// </summary>
        /// <returns></returns>

        public async Task<List<Event>> GetAllEventsAsync()
        {
            List<Event> events = new List<Event>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryGetAll, connection))
                {
                    try
                    {
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
                            int capacity = reader.GetInt32(6);
                            Event begivenheder = new (id, name, start, end, price, isMemberRequired, capacity);
                            events.Add(begivenheder);
                        }
                        return events;
                    }
                    catch (SqlException sql)
                    {
                        throw sql;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return null;
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand commmand = new SqlCommand(queryGetFromID, connection))
                {
                    try
                    {
                        commmand.Parameters.AddWithValue("@EventID", id);
                        await commmand.Connection.OpenAsync();
                        SqlDataReader reader = await commmand.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int Id = reader.GetInt32(0);
                            string eventName = reader.GetString(1);
                            DateTime dateFrom = reader.GetDateTime(2);
                            DateTime dateTo = reader.GetDateTime(3);
                            double price = reader.GetDouble(4);
                            bool isMemberRequired = reader.GetBoolean(5);
                            int capacity = reader.GetInt32(6);
                            Event begivenhed = new Event(Id, eventName, dateFrom, dateTo, price, isMemberRequired, capacity);
                            return begivenhed;
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        throw sqlEx;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }                
            }
            return null;
        }

        public async Task JoinEvent(int eventid, int memberid)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryJoin, connection);
                    command.Parameters.AddWithValue("@EventID", eventid);
                    command.Parameters.AddWithValue("@MemberID", memberid);
                    command.Connection.OpenAsync();
                    int noOfRows = await command.ExecuteNonQueryAsync();
                }
                catch (SqlException sql)
                {
                    throw sql;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<Event> RemoveEventAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryDelete, connection);
                    Event begivenhed = await GetEventByIdAsync(id);
                    command.Parameters.AddWithValue("@EventID", id);
                    command.Connection.OpenAsync();
                    int noOfRows = await command.ExecuteNonQueryAsync();
                    if (noOfRows == 1)
                    {
                        return begivenhed;
                    }                    
                }
                catch (SqlException sql)
                {
                    throw sql;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return null;
        }

        public async Task<List<int>> ReturnMembers(int eventid)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryGetMembers, connection))
                {
                    try
                    {
                        List<int> memberids = new List<int>();
                        command.Parameters.AddWithValue("@EventID", eventid);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int memberID = reader.GetInt32(1);
                            memberids.Add(memberID);
                        }
                        return memberids;
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel fejl " + ex.Message);
                    }
                    return null;
                }
            }
        }

        public async Task<bool> UpdateEventAsync(Event eEvent, int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryUpdate, connection);
                    command.Parameters.AddWithValue("@EventID", eEvent.eventID);
                    command.Parameters.AddWithValue("@Name", eEvent.name);
                    command.Parameters.AddWithValue("@EventStart", eEvent.eventStart);
                    command.Parameters.AddWithValue("@EventEnd", eEvent.eventEnd);
                    command.Parameters.AddWithValue("@Price", eEvent.price);
                    command.Parameters.AddWithValue("@IsMemberRequired", eEvent.isMemberRequired);
                    command.Parameters.AddWithValue("@Capacity", eEvent.capacity);
                    await command.Connection.OpenAsync();
                    int noOfRows = await command.ExecuteNonQueryAsync();
                    if (noOfRows == 1)
                    {
                        return true;
                    }
                }
                catch (SqlException sql)
                {
                    throw sql;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return false;
        }

        public async Task<int> CountMembers(int eventid)
        {
            using(SqlConnection connection= new SqlConnection(connectionString)) 
            {
                using(SqlCommand command= new SqlCommand(CountingMembers,connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@EventID", eventid);
                        await command.Connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while(await reader.ReadAsync())
                        {
                            int count = reader.GetInt32(0);
                            return count;
                        }

                    }
                    catch(SqlException sql) 
                    { 
                    
                    }
                    return 0;
                }
            
            
            }
        }

        public async Task<bool> DeleteEventMember(int eventid, int Memberid)
        {
            using(SqlConnection connection= new SqlConnection(connectionString)) 
            { 
                using(SqlCommand command=new SqlCommand(quaryDeleteEventMember, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@EventID", eventid);
                        command.Parameters.AddWithValue("@MemberID", Memberid);
                        command.Connection.OpenAsync();
                        int noOfRows = await command.ExecuteNonQueryAsync();
                        if(noOfRows == 1) 
                        {
                            return true;
                        
                        }

                    }
                    catch(SqlException sql)
                    {
                        throw sql;
                    }
                    catch(Exception ex) 
                    { 
                    
                    }
                    return false;


                }
            
            }
        }
    }
}

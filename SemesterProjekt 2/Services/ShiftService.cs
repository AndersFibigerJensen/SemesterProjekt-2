using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace SemesterProjekt_2.Services
{
    // Luna
    public class ShiftService : Connection, IShiftService
    {
        // Query Strings
        private string queryGetAll = "select * from Shift";
        private string queryGetFromID = "select * from Shift where shiftID = @ShiftID";
        private string queryInsert = "insert into Shifts Values(@ShiftID, @DateFrom, @DateTo, @MemberID, @EventID)";
        private string queryDelete = "delete from Shift where shiftID = @ShiftID";
        private string queryUpdate = "update Shift set ShiftID = @NewSID, DateFrom = @NewDFrom, DateTo = @NewDTo, MemberID = @NewMID, EventID = @NewEID where ShiftID = @OldSID";     
        private string querySearch = "select * from Shift where DateFrom = @DateFrom";

        public ShiftService(IConfiguration configuration) : base(configuration) { }

        // Functions
        public async Task<List<Shift>> GetAllShiftsAsync()
        {
            List<Shift> allShifts = new List<Shift>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryGetAll, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        Thread.Sleep(1000);
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        Thread.Sleep(1000);
                        while (await reader.ReadAsync())
                        {
                            int shiftID = reader.GetInt32(0);
                            DateTime dateFrom = reader.GetDateTime(1);
                            DateTime dateTo = reader.GetDateTime(2);
                            int memberID = reader.GetInt32(3);
                            int eventID = reader.GetInt32(4);
                            Shift shift = new Shift(shiftID, dateFrom, dateTo, memberID, eventID);
                            allShifts.Add(shift);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                        return null;
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("General error" + exp.Message);
                        return null;
                    }
                }
            }
            return allShifts;
        }

        public async Task<Shift> GetShiftByIdAsync(int id)
        {
            Shift s = new Shift();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryGetFromID, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ShiftID", id);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int shiftID = reader.GetInt32(0);
                            DateTime dateFrom = reader.GetDateTime(1);
                            DateTime dateTo = reader.GetDateTime(2);
                            int memberID = reader.GetInt32(3);
                            int eventID = reader.GetInt32(4);
                            s = new Shift(shiftID, dateFrom, dateTo, memberID, eventID);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                        return null;
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("General error" + exp.Message);
                        return null;
                    }
                }
            }
            return s;
        }

        public async Task<bool> AddShiftAsync(Shift shift)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryInsert, connection))
                {
                    command.Parameters.AddWithValue("@ShiftID", shift.shiftID);
                    command.Parameters.AddWithValue("@DateFrom", shift.dateFrom);
                    command.Parameters.AddWithValue("@DateTo", shift.dateTo);
                    command.Parameters.AddWithValue("@MemberID", shift.memberID);
                    command.Parameters.AddWithValue("@EventID", shift.eventID);
                    try
                    {
                        await command.Connection.OpenAsync();
                        int noOfRows = await command.ExecuteNonQueryAsync(); // To Be Used for Insert, Delete, Update
                        if (noOfRows == 1)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("General error" + exp.Message);
                    }
                }

            }
            return false;
        }

        public async Task<Shift> DeleteShiftAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryDelete, connection))
                {
                    try
                    {
                        Shift toDelete = await GetShiftByIdAsync(id);


                        command.Parameters.AddWithValue("@ShiftID", id);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        Shift deleted = await GetShiftByIdAsync(id);

                        if (toDelete != deleted)
                            return toDelete;

                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("General error" + exp.Message);
                    }
                }
            }

            return null;
        }

        public async Task<bool> UpdateShiftAsync(Shift shift, int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryUpdate, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@OldSID", id);
                        command.Parameters.AddWithValue("@NewSID", shift.shiftID);
                        command.Parameters.AddWithValue("@NewDFrom", shift.dateFrom);
                        command.Parameters.AddWithValue("@NewDTo", shift.dateTo);
                        command.Parameters.AddWithValue("@NewMID", shift.memberID);
                        command.Parameters.AddWithValue("@NewEID", shift.eventID);
                        await connection.OpenAsync();

                        int noOfRows = await command.ExecuteNonQueryAsync();
                        return noOfRows == 1;
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("General error" + exp.Message);
                    }
                }
            }
            return false;
        }

        public async Task<List<Shift>> FilterShiftsAsync(DateTime date)
        {
            List<Shift> filteredShifts = new List<Shift>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(querySearch, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@DateFrom", date);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int shiftID = reader.GetInt32(0);
                            DateTime dateFrom = reader.GetDateTime(1);
                            DateTime dateTo = reader.GetDateTime(2);
                            int memberID = reader.GetInt32(3);
                            int eventID = reader.GetInt32(4);
                            Shift shift = new Shift(shiftID, dateFrom, dateTo, memberID, eventID);
                            filteredShifts.Add(shift);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                        return null;
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("General error" + exp.Message);
                        return null;
                    }
                }
            }
            return filteredShifts;
        }
    }
}

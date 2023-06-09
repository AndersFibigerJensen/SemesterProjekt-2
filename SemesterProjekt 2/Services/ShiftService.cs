﻿using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Windows.Input;

namespace SemesterProjekt_2.Services
{
    // Luna
    public class ShiftService : Connection, IShiftService
    {
        // Query Strings
        private string queryGetAll = "select * from Shift";
        private string queryGetFromID = "select * from Shift where shiftID = @ShiftID";
        private string queryInsert = "insert into Shift (ShiftID, DateFrom, DateTo, Type) Values(@ShiftID, @DateFrom, @DateTo, @ShiftType)";
        private string queryDelete = "delete from Shift where shiftID = @ShiftID";
        private string queryUpdate = "update Shift set ShiftID = @NewSID, DateFrom = @NewDFrom, DateTo = @NewDTo, Type = @NewType where ShiftID = @OldSID";     
        private string querySearch = "select * from Shift where (datepart(yy, DateFrom) = @Year and datepart(mm, DateFrom) = @Month and datepart(dd, DateFrom) = @Day)";
        private string queryGetAllFromMemberID = "select * from Shift where MemberID = @MemberID";
        private string queryGetAllFromEventID = "select * from Shift where EventID = @EventID";
        private string queryUpdateMemberID = "update Shift set MemberID = @NewMID where ShiftID = @OldSID";
        private string queryUpdateEventID = "update Shift set EventID = @NewEID where ShiftID = @OldSID";

        public ShiftService(IConfiguration configuration) : base(configuration) { }
        public ShiftService(string connectionstring) : base(connectionstring) { }

        // Functions

        /// <summary>
        /// Gets a list of all shifts currently in the database
        /// </summary>
        /// <returns>A list containing all shifts currently in the database</returns>
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
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int shiftID = reader.GetInt32(0);
                            DateTime dateFrom = reader.GetDateTime(1);
                            DateTime dateTo = reader.GetDateTime(2);

                            int? memberID = null;
                            if (!reader.IsDBNull(i: 3))
                            {
                                memberID = reader.GetInt32(i: 3);
                            }
                            int? eventID = null;
                            if (!reader.IsDBNull(i: 4))
                            {
                                eventID = reader.GetInt32(i: 4);
                            }

                            Shift.Type shiftType = (Shift.Type)reader.GetInt32(5);

                            //int memberID = reader.GetInt32(3);
                            //int eventID = reader.GetInt32(4);
                            Shift shift = new Shift(shiftID, dateFrom, dateTo, memberID, eventID, shiftType);
                            allShifts.Add(shift);
                        }
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
            return allShifts;
        }

        /// <summary>
        /// Gets the shift currently assigned to the input ID
        /// </summary>
        /// <param name="id">ID of the shift we're searching for</param>
        /// <returns>The shift assigned to the input ID</returns>
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
                            int? memberID = null;
                            if (!reader.IsDBNull(i: 3))
                            {
                                memberID = reader.GetInt32(i: 3);
                            }
                            int? eventID = null;
                            if (!reader.IsDBNull(i: 4))
                            {
                                eventID = reader.GetInt32(i: 4);
                            }
                            Shift.Type shiftType = (Shift.Type)reader.GetInt32(5);
                            s = new Shift(shiftID, dateFrom, dateTo, memberID, eventID, shiftType);
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

        /// <summary>
        /// Adds a shift to the database
        /// </summary>
        /// <param name="shift">Shift to be added to the database</param>
        /// <returns>Whether or not the process was successful (true if yes, false if no)</returns>
        public async Task<bool> AddShiftAsync(Shift shift)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryInsert, connection))
                {
                    command.Parameters.AddWithValue("@ShiftID", shift.ShiftID);
                    command.Parameters.AddWithValue("@DateFrom", shift.DateFrom);
                    command.Parameters.AddWithValue("@DateTo", shift.DateTo);
                    command.Parameters.AddWithValue("@ShiftType", (int)shift.ShiftType);
                    //command.Parameters.AddWithValue("@MemberID", shift.MemberID);
                    //command.Parameters.AddWithValue("@EventID", shift.EventID);
                    try
                    {
                        await command.Connection.OpenAsync();
                        int noOfRows = await command.ExecuteNonQueryAsync(); // To Be Used for Insert, Delete, Update
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

        /// <summary>
        /// Deletes a shift from the database
        /// </summary>
        /// <param name="id">ID of the shift to be deleted</param>
        /// <returns>Deleted shift</returns>
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

        /// <summary>
        /// Changes the parameters of a shift
        /// </summary>
        /// <param name="shift">Shift containing the new parameters</param>
        /// <param name="id">ID of the old shift, the one to be updated</param>
        /// <returns>Whether or not the task process was successful (true if yes, false if no)</returns>
        public async Task<bool> UpdateShiftAsync(Shift shift, int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryUpdate, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@OldSID", id);
                        command.Parameters.AddWithValue("@NewSID", shift.ShiftID);
                        command.Parameters.AddWithValue("@NewDFrom", shift.DateFrom);
                        command.Parameters.AddWithValue("@NewDTo", shift.DateTo);
                        command.Parameters.AddWithValue("@NewType", (int)shift.ShiftType);
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

        public async Task<bool> UpdateMemberIDAsync(Shift shift, int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryUpdateMemberID, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@OldSID", id);
                        if (shift.MemberID == null)
                        {
                            command.Parameters.AddWithValue("@NewMID", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@NewMID", shift.MemberID);
                        }
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

        public async Task<bool> UpdateEventIDAsync(Shift shift, int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryUpdateEventID, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@OldSID", id);

                        if (shift.EventID == null)
                        {
                            command.Parameters.AddWithValue("@NewEID", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@NewEID", shift.EventID);
                        }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<List<Shift>> FilterShiftsAsync(DateTime date)
        {
            List<Shift> filteredShifts = new List<Shift>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(querySearch, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@Year", date.Year);
                        command.Parameters.AddWithValue("@Month", date.Month);
                        command.Parameters.AddWithValue("@Day", date.Day);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int shiftID = reader.GetInt32(0);
                            DateTime dateFrom = reader.GetDateTime(1);
                            DateTime dateTo = reader.GetDateTime(2);
                            int? memberID = null;
                            if (!reader.IsDBNull(i: 3))
                            {
                                memberID = reader.GetInt32(i: 3);
                            }
                            int? eventID = null;
                            if (!reader.IsDBNull(i: 4))
                            {
                                eventID = reader.GetInt32(i: 4);
                            }
                            Shift.Type shiftType = (Shift.Type)reader.GetInt32(5);
                            Shift shift = new Shift(shiftID, dateFrom, dateTo, memberID, eventID, shiftType);
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

        public async Task<List<Shift>> GetAllShiftsByMemberIdAsync(int memberid)
        {
            List<Shift> filteredShifts = new List<Shift>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryGetAllFromMemberID, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@MemberID", memberid);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int shiftID = reader.GetInt32(0);
                            DateTime dateFrom = reader.GetDateTime(1);
                            DateTime dateTo = reader.GetDateTime(2);
                            int? memberID = null;
                            if (!reader.IsDBNull(i: 3))
                            {
                                memberID = reader.GetInt32(i: 3);
                            }
                            int? eventID = null;
                            if (!reader.IsDBNull(i: 4))
                            {
                                eventID = reader.GetInt32(i: 4);
                            }
                            Shift.Type shiftType = (Shift.Type)reader.GetInt32(5);
                            Shift shift = new Shift(shiftID, dateFrom, dateTo, memberID, eventID, shiftType);
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

        public async Task<List<Shift>> GetAllShiftsByEventIdAsync(int eventid)
        {
            List<Shift> filteredShifts = new List<Shift>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryGetAllFromEventID, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@EventID", eventid);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int shiftID = reader.GetInt32(0);
                            DateTime dateFrom = reader.GetDateTime(1);
                            DateTime dateTo = reader.GetDateTime(2);
                            int? memberID = null;
                            if (!reader.IsDBNull(i: 3))
                            {
                                memberID = reader.GetInt32(i: 3);
                            }
                            int? eventID = null;
                            if (!reader.IsDBNull(i: 4))
                            {
                                eventID = reader.GetInt32(i: 4);
                            }
                            Shift.Type shiftType = (Shift.Type)reader.GetInt32(5);
                            Shift shift = new Shift(shiftID, dateFrom, dateTo, memberID, eventID, shiftType);
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

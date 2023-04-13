using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using Microsoft.Data.SqlClient;

namespace SemesterProjekt_2.Services
{
    //Luna
    public class ShiftService :Connection, IShiftService
    {
        // Query Strings
        private string queryGetAll = "select * from Shift";
        private string queryGetFromID = "select * from Shift where shiftID = @ShiftID";
        private string queryInsert = "insert into Shifts Values(@ShiftID, @DateFrom, @DateTo, @MemberID)";
        private string queryDelete = "delete from Shift where shiftID = @ShiftID";
        private string queryUpdate = "update Shift set ShiftID = @NewSID, DateFrom = @NewDFrom, DateTo = @NewDTo, MemberID = @NewMID where ShiftID = @OldSID";     
        private string querySearch = "nyi";

        public ShiftService(IConfiguration configuration):base(configuration)
        {

        }

        // Functions
        public Task<List<Shift>> GetAllShiftsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Shift> GetShiftByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddShiftAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteShiftAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateShiftAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Member>> FilterShiftsAsync(DateTime dtFrom, DateTime dtTo)
        {
            throw new NotImplementedException();
        }
    }
}

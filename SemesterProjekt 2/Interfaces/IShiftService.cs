using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Interfaces
{

    //Luna
    public interface IShiftService
    {
        public Task<List<Shift>> GetAllShiftsAsync();

        public Task<Shift> GetShiftByIdAsync(int id);

        public Task AddShiftAsync();

        public Task DeleteShiftAsync(int id);

        public Task UpdateShiftAsync(int id);

        public Task<List<Member>> FilterShiftsAsync(DateTime date);

    }
}

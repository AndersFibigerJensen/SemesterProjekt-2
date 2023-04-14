using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Interfaces
{

    //Luna
    public interface IShiftService
    {
        public Task<List<Shift>> GetAllShiftsAsync();

        public Task<Shift> GetShiftByIdAsync(int id);

        public Task<bool> AddShiftAsync(Shift shift);

        public Task<Shift> DeleteShiftAsync(int id);

        public Task<bool> UpdateShiftAsync(Shift shift, int id);

        public Task<List<Member>> FilterShiftsAsync(DateTime date);

    }
}

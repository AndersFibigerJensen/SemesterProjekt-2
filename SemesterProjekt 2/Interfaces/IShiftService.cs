using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Interfaces
{

    //Luna
    public interface IShiftService
    {
        /// <summary>
        /// Gets a list of all shifts currently in the database
        /// </summary>
        /// <returns>A list containing all shifts currently in the database</returns>
        public Task<List<Shift>> GetAllShiftsAsync();

        /// <summary>
        /// Gets the shift currently assigned to the input ID
        /// </summary>
        /// <param name="id">ID of the shift we're searching for</param>
        /// <returns>The shift assigned to the input ID</returns>
        public Task<Shift> GetShiftByIdAsync(int id);

        /// <summary>
        /// Adds a shift to the database
        /// </summary>
        /// <param name="shift">Shift to be added to the database</param>
        /// <returns>Whether or not the process was successful (true if yes, false if no)</returns>
        public Task<bool> AddShiftAsync(Shift shift);

        /// <summary>
        /// Deletes a shift from the database
        /// </summary>
        /// <param name="id">ID of the shift to be deleted</param>
        /// <returns>Deleted shift</returns>
        public Task<Shift> DeleteShiftAsync(int id);

        /// <summary>
        /// Changes the parameters of a shift
        /// </summary>
        /// <param name="shift">Shift containing the new parameters</param>
        /// <param name="id">ID of the old shift, the one to be updated</param>
        /// <returns>Whether or not the task process was successful (true if yes, false if no)</returns>
        public Task<bool> UpdateShiftAsync(Shift shift, int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public Task<List<Shift>> FilterShiftsAsync(DateTime date);

    }
}

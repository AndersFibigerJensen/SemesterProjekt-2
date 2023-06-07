using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using SemesterProjekt_2.Services;

namespace JordnærTest2
{
    [TestClass]
    public class UnitTest3
    {
        private string connection = Secret1.ConnectionString;
        [TestMethod]
        public async Task AddShiftTestAsync()
        {
            // ARRANGE
            ShiftService sService = new ShiftService(connection);
            List<Shift> shifts = sService.GetAllShiftsAsync().Result;

            // ACT
            int countBefore = shifts.Count;
            DateTime DT1 = new DateTime(2023, 05, 13, 8, 0, 0);
            DateTime DT2 = new DateTime(2023, 05, 13, 12, 0, 0);
            Shift newShift = new Shift(888, DT1, DT2, null, null, Shift.Type.CafévagtFøl);
            sService.AddShiftAsync(newShift);
            sService.DeleteShiftAsync(888);
            int countAfter = shifts.Count;

            // ASSERT
            Assert.AreEqual(countBefore, countAfter);
        }

    }
}

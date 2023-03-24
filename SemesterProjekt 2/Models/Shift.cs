namespace SemesterProjekt_2.Models
{
    public class Shift
    {
        private static int _shiftID;
        private DateTime _dateFrom;
        private DateTime _dateTo;
        private Member _assignedMember;

        public Shift(DateTime DateFrom, DateTime DateTo, Member AssignedMember)
        {
            _shiftID++;
        }

    }
}

namespace SemesterProjekt_2.Models
{
    public class Shift
    {
        private static int _shiftID;
        private DateTime _dateFrom;
        private DateTime _dateTo;
        private Member _assignedMember;

        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
        public Member assignedMember { get; set; }

        public Shift()
        {

        }

        public Shift(DateTime DateFrom, DateTime DateTo, Member AssignedMember)
        {
            _shiftID++;
            _dateFrom = DateFrom;
            _dateTo = DateTo;
            _assignedMember = AssignedMember;
        }

    }
}

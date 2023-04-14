namespace SemesterProjekt_2.Models
{
    public class Shift
    {
        private static int _shiftID;
        private DateTime _dateFrom;
        private DateTime _dateTo;

        public int shiftID { get { return _shiftID; } set { _shiftID = value; }}
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
        public int memberID { get; set; }
        public int eventID { get; set; }

        public Member assignedMember { get; set; }
        public Event assignedEvent { get; set; }


        public Shift()
        {

        }

        public Shift(int ShiftID, DateTime DateFrom, DateTime DateTo, int MemberID, int EventID)
        {
            _shiftID = ShiftID;
            _dateFrom = DateFrom;
            _dateTo = DateTo;
            memberID = MemberID;
            eventID = EventID;

            // Once the ability to search for members/events is implemented, some more stuff can be done here
        }

    }
}

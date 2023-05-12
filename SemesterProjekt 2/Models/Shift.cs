namespace SemesterProjekt_2.Models
{
    public class Shift
    {
        public int ShiftID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int? MemberID { get; set; }
        public int? EventID { get; set; }
        public Member assignedMember { get; set; }
        public Event assignedEvent { get; set; }


        public Shift()
        {

        }

        public Shift(int shiftID, DateTime dateFrom, DateTime dateTo, int? memberID, int? eventID)
        {
            ShiftID = shiftID;
            DateFrom = dateFrom;
            DateTo = dateTo;
            MemberID = memberID;
            EventID = eventID;
        }

    }
}

namespace SemesterProjekt_2.Models
{
    public class Shift
    {
        // Luna

        public int ShiftID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int? MemberID { get; set; }
        public int? EventID { get; set; }
        public enum Type
        {
            Cafévagt = 1,
            CafévagtFøl = 2,
            Bager = 3,
            BagerFøl = 4,
            Andet = 5,
        }
        public Type ShiftType { get; set; }

        public Shift(int shiftID, DateTime dateFrom, DateTime dateTo, int? memberID, int? eventID, Type type)
        {
            ShiftID = shiftID;
            DateFrom = dateFrom;
            DateTo = dateTo;
            MemberID = memberID;
            EventID = eventID;
            ShiftType = type;
        }

        public Shift() { }
    }
}

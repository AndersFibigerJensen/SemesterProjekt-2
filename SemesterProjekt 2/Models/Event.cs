namespace SemesterProjekt_2.Models
{
    //Adam
    public class Event
    {
        public int eventID { get; set; }
        public string name { get; set; }
        public DateTime eventStart { get; set; }
        public DateTime eventEnd { get; set; }
        public double price { get; set; }
        public bool isMemberRequired { get; set; }

        public Event()
        {

        }

        public Event(int eventid, string Name, DateTime EventStart, DateTime EventEnd, double Price, bool IsMemberRequired)
        {
            eventID = eventid;
            name = Name;
            eventStart = EventStart;
            eventEnd = EventEnd;
            price = Price;
            isMemberRequired = IsMemberRequired;
        }

    }
}

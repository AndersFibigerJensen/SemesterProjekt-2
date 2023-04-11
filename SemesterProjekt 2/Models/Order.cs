using System.Xml.Schema;

namespace SemesterProjekt_2.Models
{
    public class Order
    {
        //Anders

        public int OrderID { get; set; }

        public List<Meal> Meals { get; set; }

        public Member member { get; set; }

        public DateTime PurchaseDate { get; set; }

        public  double TotalPrice { get; set; }

        public Order()
        {

        }
    }
}

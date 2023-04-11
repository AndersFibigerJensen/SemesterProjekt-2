namespace SemesterProjekt_2.Models
{
    //Adam
    public class Meal
    {
        public int MealID { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public List<Foodstuff> Ingredients { get; set; }

        public Meal()
        {

        }


    }
}

namespace PubLibIS_BLL.Model
{
    public class Article
    {
        public int Id { get; set; }
        public string Capation { get; set; }


        public  Author Author { get; set; }
        public  PeriodicalEdition PeriodicalEdition { get; set; }
    }
}

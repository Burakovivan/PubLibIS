namespace PubLibIS.DAL.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Capation { get; set; }


        public virtual Author Author { get; set; }
        public virtual PeriodicalEdition PeriodicalEdition { get; set; }
    }
}

namespace PubLibIS.ViewModels
{
    public class PeriodicalTypeViewModel
    {
        public PeriodicalTypeViewModel() { }
        public PeriodicalTypeViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
}
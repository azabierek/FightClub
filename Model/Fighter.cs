namespace FightClub.Model
{
    public class Fighter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }
        public double Weight { get; set; }
        public int BirthYear { get; set; }
        public int FirstShowUpYear { get; set; }
        public string PhotoSource { get; set; }
        public Belt Belt { get; set; }
        public Stripe Stripe { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}

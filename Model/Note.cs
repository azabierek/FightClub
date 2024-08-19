namespace FightClub.Model
{
    public class Note
    {
        public int Id { get; set; }
        public DateTime InsertedDate { get; set; } = DateTime.Now;
        public string NoteFromTheCoach { get; set; }
    }
}

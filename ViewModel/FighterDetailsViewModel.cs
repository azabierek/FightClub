using FightClub.Model;
using FightClub.Services;
using FightClub.Static;
using System.Text;

namespace FightClub.ViewModel
{
    [QueryProperty("Fighter", "Fighter")]
    [QueryProperty("Notes", "Notes")]
    public class FighterDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("Fighter"))
            {
                Fighter = query["Fighter"] as Fighter;
                BeltImageString = SetBeltImageString();

                Notes = query["Notes"] as List<Note>;
            }
        }

        private FightClubRepository _repo;
        private Fighter fighter;
        private string beltImageString;
        private bool detailsVisible;
        private bool notesVisible;
        private List<Note> notes;
        public Command AddNoteToTheFighterCommand { get; }


        public Fighter Fighter
        {
            get => fighter;
            set 
            {
                if (fighter == value)
                    return;

                fighter = value;
                OnPropertyChanged(nameof(Fighter));
            }
        }
        public string BeltImageString
        {
            get => beltImageString;
            set 
            {
                if (beltImageString == value)
                    return;

                beltImageString = value; 
                OnPropertyChanged(nameof(BeltImageString));
            }
        }
        public bool NotesVisible
        {
            get => notesVisible;
            set 
            {
                if (notesVisible == value)
                    return;

                notesVisible = value;

                OnPropertyChanged(nameof(NotesVisible));
            }
        }
        public bool DetailsVisible
        {
            get => detailsVisible;
            set
            {
                if (detailsVisible == value)
                    return;

                detailsVisible = value;
                NotesVisible = !value;
                OnPropertyChanged(nameof(DetailsVisible));
            }
        }
        public List<Note> Notes
        {
            get => notes;
            set
            {
                if (notes == value)
                    return;

                notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }

        public FighterDetailsViewModel(FightClubRepository repo) 
        {
            _repo = repo;

            AddNoteToTheFighterCommand = new Command(async () => await AddNoteToTheFighterAsync());

            NotesVisible = true;
        }

        private async Task AddNoteToTheFighterAsync()
        {
            IsBusy = true;
            try
            {
                var insertedNote = await Shell.Current.DisplayPromptAsync("NOWA NOTATKA", $"Wpisz poniżej nową notatkę przypisaną do zawodnika: {Fighter.Name} {Fighter.Surname}", "OK", "ANULUJ", "Tutaj napisz ...");
                
                if (insertedNote == null)
                    return;

                if (insertedNote.Length > 5)
                {
                    Note note = new Note
                    {
                        IdFighter = Fighter.Id,
                        InsertedDate = DateTime.Now,
                        NoteFromTheCoach = insertedNote
                    };

                    if (await _repo.AddNoteToFighter(note, Fighter))
                        await GetNotesAsync(Fighter.Id);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Notatka zbyt krótka", "Minimalna długość notatki to 6 znaków.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Błąd", "Nie udało się dodać notatki.", "OK");

#if DEBUG
                Console.WriteLine(ex.ToString());
#endif
            }
            finally
            {
                IsBusy = false;
            }


        }

        private async Task GetNotesAsync(int fighterId)
        {
            Notes = await _repo.GetNotesByFighterId(fighterId) as List<Note>;
        }
        private string SetBeltImageString()
        {
            StringBuilder sb = new StringBuilder();

            if (Fighter.Belt == Belt.Biały)
                sb.Append("white");
            if (Fighter.Belt == Belt.Niebieski)
                sb.Append("blue");
            if (Fighter.Belt == Belt.Purpurowy)
                sb.Append("purple");
            if (Fighter.Belt == Belt.Brązowy)
                sb.Append("brown");
            if (Fighter.Belt == Belt.Czarny)
                sb.Append("black");

            if (Fighter.Stripe == Stripe.Zero)
                sb.Append("0.png");
            if (Fighter.Stripe == Stripe.Jeden)
                sb.Append("1.png");
            if (Fighter.Stripe == Stripe.Dwa)
                sb.Append("2.png");
            if (Fighter.Stripe == Stripe.Trzy)
                sb.Append("3.png");
            if (Fighter.Stripe == Stripe.Cztery)
                sb.Append("4.png");

            return sb.ToString();
        }
    }
}

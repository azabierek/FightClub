using FightClub.Model;
using FightClub.Services;
using FightClub.Static;
using System.Windows.Input;

namespace FightClub.ViewModel
{
    public class AddFighterViewModel : BaseViewModel
    {
        private FightClubRepository _repo;

        private string name;
        private string surname;
        private string nickname;
        private string weight;
        private string birthYear;
        private string firstShowUpYear;
        private string photoSource;
        private Belt belt;
        private Stripe stripe;
        private string note;
        private string selectedImagePath;

        public AddFighterViewModel(FightClubRepository repo)
        {
            _repo = repo;

            PickPhotoCommand = new Command(async () => await PickPhotoAsync());
            AddFighterCommand = new Command(async () => await AddFigterAsync());
        }

        public string Name
        {
            get => name;
            set 
            {
                if(name == value) 
                    return;

                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Surname
        {
            get => surname;
            set
            {
                if (surname == value)
                    return;

                surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }
        public string Nickname
        {
            get => nickname;
            set
            {
                if (nickname == value)
                    return;

                nickname = value;
                OnPropertyChanged(nameof(Nickname));
            }
        }
        public string Weight
        {
            get => weight;
            set 
            {
                if (weight == value)
                    return;

                weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }
        public string BirthYear
        {
            get => birthYear;
            set 
            {
                if (birthYear == value)
                    return;

                birthYear = value;
                OnPropertyChanged(nameof(BirthYear));
            }
        }
        public string FirstShowUpYear
        {
            get => firstShowUpYear;
            set
            {
                if (firstShowUpYear == value)
                    return;

                firstShowUpYear = value;
                OnPropertyChanged(nameof(FirstShowUpYear));
            }
        }
        public string PhotoSource
        {
            get => photoSource;
            set
            {
                if (photoSource == value)
                    return;

                photoSource = value;
                OnPropertyChanged(nameof(PhotoSource));
            }
        }
        public Belt Belt
        {
            get => belt;
            set
            {
                if (belt == value)
                    return;

                belt = value;
                OnPropertyChanged(nameof(Belt));
            }
        }
        public Stripe Stripe
        {
            get => stripe;
            set
            {
                if (stripe == value)
                    return;

                stripe = value;
                OnPropertyChanged(nameof(Stripe));
            }
        }
        public string Note
        {
            get => note;
            set
            {
                if (note == value)
                    return;

                note = value;
                OnPropertyChanged(nameof(Note));
            }
        }
        public string SelectedImagePath
        {
            get => selectedImagePath;
            set
            {
                selectedImagePath = value;
                OnPropertyChanged(nameof(SelectedImagePath));
            }
        }

        public ICommand PickPhotoCommand { get; }
        public ICommand AddFighterCommand { get; }

        private async Task PickPhotoAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    FileResult photo = await MediaPicker.PickPhotoAsync();

                    if (photo != null)
                    {
                        var filePath = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                        using (var stream = await photo.OpenReadAsync())
                        using (var newStream = File.OpenWrite(filePath))
                            await stream.CopyToAsync(newStream);

                        SelectedImagePath = filePath;

                    }
                }
                catch (Exception ex)
                {
                    // Obsługa błędów
                    await Application.Current.MainPage.DisplayAlert("Błąd", $"Nie udało się załadować zdjęcia: {ex.Message}", "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
        private async Task AddFigterAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    if (FighterValidator.CheckName(Name))
                    {
                        if (FighterValidator.CheckSurname(Surname))
                        {
                            if (FighterValidator.CheckYear(BirthYear))
                            {
                                if (FighterValidator.CheckYear(FirstShowUpYear) && FighterValidator.CheckFirstTraining(BirthYear, FirstShowUpYear))
                                {
                                    if (FighterValidator.CheckWeight(Weight))
                                    {
                                        var coachNote = new Note()
                                        {
                                            InsertedDate = DateTime.Now,
                                            NoteFromTheCoach = "Dodanie zawodnika. " + Note
                                        };

                                        var collection = new List<Note>();
                                        collection.Add(coachNote);

                                        var standardPath = Path.Combine(FileSystem.AppDataDirectory, "zt.png");

                                        Fighter fighter = new Fighter()
                                        {
                                            Name = Name.CapitalizeFirstLetter(),
                                            Surname = Surname.CapitalizeFirstLetter(),
                                            Nickname = Nickname.CapitalizeFirstLetter(),
                                            BirthYear = Convert.ToInt32(BirthYear),
                                            Belt = Belt,
                                            Stripe = Stripe,
                                            FirstShowUpYear = Convert.ToInt32(FirstShowUpYear),
                                            Weight = Convert.ToDouble(Weight),
                                            PhotoSource = SelectedImagePath == null ? standardPath : SelectedImagePath,
                                            //Notes = collection
                                        };

                                        if (await _repo.AddFighter(fighter, coachNote))
                                        {
                                            await Application.Current.MainPage.DisplayAlert("OK", $"Zawodnik {Name} {Surname} dodany!", "OK");
                                            await AppShell.Current.GoToAsync("..");
                                        }
                                        else
                                            await Application.Current.MainPage.DisplayAlert("Błąd", $"Nie udało się dodać zawodnika", "OK");
                                    }
                                    else
                                        await Application.Current.MainPage.DisplayAlert("WAGA", $"Uzupełnij poprawnie wagę zawodnika!", "OK");
                                }
                                else
                                    await Application.Current.MainPage.DisplayAlert("PIERWSZY TRENING", $"Uzupełnij poprawnie rok pierwszego treningu zawodnika!", "OK");
                            }
                            else
                                await Application.Current.MainPage.DisplayAlert("WIEK", $"Uzupełnij poprawnie wiek zawodnika!", "OK");
                        }
                        else
                            await Application.Current.MainPage.DisplayAlert("NAZWISKO", $"Uzupełnij poprawnie nazwisko zawodnika!", "OK");
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("IMIĘ", $"Uzupełnij poprawnie imię zawodnika!", "OK");
                }
                catch (Exception)
                {

                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
    }
}

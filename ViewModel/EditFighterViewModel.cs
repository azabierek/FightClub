using FightClub.Model;
using FightClub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightClub.ViewModel
{
    [QueryProperty("Fighter", "Fighter")]
    public class EditFighterViewModel : BaseViewModel, IQueryAttributable
    {
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("Fighter"))
            {
                Fighter = query["Fighter"] as Fighter;
                SelectedImagePath = Fighter.PhotoSource;
            }
        }
        public EditFighterViewModel(FightClubRepository repo)
        {
            _repo = repo;

            SaveChangesCommand = new Command(async () => await SaveChangesAsync());
            SelectPhotoCommand = new Command(async () => await SelectPhotoAsync());
        }

        private readonly FightClubRepository _repo;

        private Fighter fighter;
        private string selectedImagePath;

        public Fighter Fighter
        {
            get => fighter;
            set 
            {
                fighter = value;
                OnPropertyChanged(nameof(Fighter));
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


        public Command SaveChangesCommand { get; }
        public Command SelectPhotoCommand { get; }

        private async Task SaveChangesAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    
                    await _repo.UpdateFighter(Fighter);
                    await Shell.Current.GoToAsync("../..");
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
        private async Task SelectPhotoAsync()
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
    }
}

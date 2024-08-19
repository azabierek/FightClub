using FightClub.Model;
using FightClub.Services;
using FightClub.Static;
using FightClub.View;

namespace FightClub.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private FightClubRepository _repo;

        private int selectedFilterIndex;
        private int selectedBeltIndex;

        private bool isFilterByBeltVisible;
        private bool isFilterByNotBeltVisible;
        private bool isNameFilterButtonVisible;
        private bool isSurnameFilterButtonVisible;
        private bool isNicknameFilterButtonVisible;
        private bool isBeltWeightFilterButtonVisible;

        private string nameInput;
        private string nicknameInput;
        private string surnameInput;
        private Belt? beltInput;
        private int weightFromInput;
        private int weightToInput;

        private List<Fighter> fighters;
        private List<string> filterByList;
        private List<Belt> beltsList;

        public Command GetFightersCommand { get; }
        public Command GoToFightersDetailsCommand { get; }
        public Command FindFightersByNameCommand { get; }
        public Command FindFightersBySurnameCommand { get; }
        public Command FindFightersByNicknameCommand { get; }
        public Command FindFightersByBeltAndWeightCommand { get; }


        public MainPageViewModel(FightClubRepository repo)
        {
            _repo = repo;
            GetFightersCommand = new Command(async () => await GetFightersAsync());
            GoToFightersDetailsCommand = new Command<Fighter>(async (fighter) => await GoToFighterDetailsAsync(fighter));
            FindFightersByNameCommand = new Command(async () => await GetFightersByNameAsync());
            FindFightersBySurnameCommand = new Command(async () => await GetFightersBySurnameAsync());
            FindFightersByNicknameCommand = new Command(async () => await GetFightersByNicknameAsync());
            FindFightersByBeltAndWeightCommand = new Command(async () => await GetFightersByBeltAndWeightAsync());
            FilterByList = ElementsFromList.FilterByList;
            BeltsList = ElementsFromList.BeltsList;


            SelectedFilterIndex = -1;
            BeltInput = null;

            WeightToInput = 140;
        }

        public int SelectedFilterIndex
        {
            get { return selectedFilterIndex; }
            set
            {
                if (selectedFilterIndex == value)
                    return;

                BeltInput = null;
                IsFilterByBeltVisible = false;
                IsFilterByNotBeltVisible = false;

                IsNameFilterButtonVisible = false;
                IsSurnameFilterButtonVisible = false;
                IsNicknameFilterButtonVisible = false;
                selectedFilterIndex = value;


                if (value == 3)
                {
                    IsFilterByBeltVisible = true;
                }
                else
                {
                    IsFilterByNotBeltVisible = true;

                    if (value == 0)
                        IsNameFilterButtonVisible = true;
                    if (value == 1)
                        IsSurnameFilterButtonVisible = true;
                    if (value == 2)
                        IsNicknameFilterButtonVisible = true;
                }
            }
        }
        public int SelectedBeltIndex
        {
            get => selectedBeltIndex; 
            set
            {
                if (selectedBeltIndex == value)
                    return;

                selectedBeltIndex = value;
                OnPropertyChanged(nameof(selectedBeltIndex));
            }
        }
        public bool IsFilterByBeltVisible
        {
            get => isFilterByBeltVisible;
            set
            {
                if (isFilterByBeltVisible == value)
                    return;

                isFilterByBeltVisible = value;
                OnPropertyChanged(nameof(IsFilterByBeltVisible));
            }
        }
        public bool IsFilterByNotBeltVisible
        {
            get => isFilterByNotBeltVisible;
            set
            {
                if (isFilterByNotBeltVisible == value)
                    return;

                isFilterByNotBeltVisible = value;
                OnPropertyChanged(nameof(IsFilterByNotBeltVisible));
            }
        }

        public bool IsNameFilterButtonVisible
        {
            get => isNameFilterButtonVisible; 
            set 
            {
                if(isNameFilterButtonVisible == value) 
                    return;
                
                isNameFilterButtonVisible = value;
                OnPropertyChanged(nameof(IsNameFilterButtonVisible));
            }
        }
        public bool IsSurnameFilterButtonVisible
        {
            get => isSurnameFilterButtonVisible;
            set
            {
                if (isSurnameFilterButtonVisible == value)
                    return;

                isSurnameFilterButtonVisible = value;
                OnPropertyChanged(nameof(IsSurnameFilterButtonVisible));
            }
        }
        public bool IsNicknameFilterButtonVisible
        {
            get => isNicknameFilterButtonVisible;
            set
            {
                if (isNicknameFilterButtonVisible == value)
                    return;

                isNicknameFilterButtonVisible = value;
                OnPropertyChanged(nameof(IsNicknameFilterButtonVisible));
            }
        }
        public bool IsBeltWeightFilterButtonVisible
        {
            get => isBeltWeightFilterButtonVisible;
            set
            {
                if (isBeltWeightFilterButtonVisible == value)
                    return;

                isBeltWeightFilterButtonVisible = value;
                OnPropertyChanged(nameof(IsBeltWeightFilterButtonVisible));
            }
        }
        public List<Fighter> Fighters
        {
            get => fighters;
            set
            {
                if (fighters == value)
                    return;

                fighters = value;
                OnPropertyChanged(nameof(Fighters));
            }
        }
        public List<string> FilterByList
        {
            get => filterByList;
            set
            {
                if (value == filterByList)
                    return;

                filterByList = value;
                OnPropertyChanged(nameof(FilterByList));
            }
        }
        public List<Belt> BeltsList
        {
            get => beltsList;
            set
            {
                if (value == beltsList)
                    return;

                beltsList = value;
                OnPropertyChanged(nameof(BeltsList));
            }
        }
        private async Task GoToFighterDetailsAsync(Fighter fighter)
        {
            if (fighter == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(FighterDetailsPage)}", true,
                new Dictionary<string, object>
                {
                    {"Fighter", fighter }
                });
        }
        private async Task GetFightersAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                Fighters = await _repo.GetFighters();

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Error with Get Fighters", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task GetFightersByNameAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                Fighters = await _repo.FindFightersByName(NameInput);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Error with Get Fighters", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task GetFightersBySurnameAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                Fighters = await _repo.FindFightersBySurname(SurnameInput);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Error with Get Fighters", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task GetFightersByNicknameAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                Fighters = await _repo.FindFightersByNickname(NicknameInput);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Error with Get Fighters", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task GetFightersByBeltAndWeightAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                Fighters = await _repo.FindFightersByBeltAndWeight(BeltInput, WeightFromInput, WeightToInput);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Error with Get Fighters", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        public string NameInput
        {
            get => nameInput;
            set
            {
                if (value == nameInput)
                    return;

                nameInput = value;
                OnPropertyChanged(nameof(NameInput));
            }
        }
        public string NicknameInput
        {
            get => nicknameInput;
            set
            {
                if (value == nicknameInput)
                    return;

                nicknameInput = value;
                OnPropertyChanged(nameof(NicknameInput));

            }
        }
        public string SurnameInput
        {
            get => surnameInput;
            set
            {
                if (surnameInput == value)
                    return;

                surnameInput = value;
                OnPropertyChanged(nameof(SurnameInput));
            }
        }
        public Belt? BeltInput
        {
            get => beltInput;
            set
            {
                if (value == beltInput)
                    return;

                beltInput = value;
                OnPropertyChanged(nameof(BeltInput));
            }
        }
        public int WeightFromInput
        {
            get => weightFromInput;
            set
            {
                if (value == weightFromInput)
                    return;

                if (value < 0)
                {
                    Shell.Current.DisplayAlert("Wprowadzona waga.", "Wartość musi być dodatnia.", "OK");
                    return;
                }

                weightFromInput = value;
                OnPropertyChanged(nameof(WeightFromInput));
            }
        }
        public int WeightToInput
        {
            get => weightToInput;
            set
            {
                if (value == weightToInput)
                    return;

                if (value < 0)
                {
                    Shell.Current.DisplayAlert("Wprowadzona waga.", "Wartość musi być dodatnia.", "OK");
                    return;
                }
                weightToInput = value;
                OnPropertyChanged(nameof(WeightToInput));
            }
        }

    }
}

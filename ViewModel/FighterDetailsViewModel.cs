using FightClub.Model;

namespace FightClub.ViewModel
{
    [QueryProperty("Fighter", "Fighter")]
    public class FighterDetailsViewModel : BaseViewModel
    {
        private Fighter fighter;

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

        public FighterDetailsViewModel() 
        {

        }

        
    
    }
}

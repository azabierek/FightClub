using FightClub.Model;
using FightClub.Static;
using System.ComponentModel;

namespace FightClub.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public BaseViewModel()
        {
            BeltsList = ElementsFromList.BeltsList;
            StripeList = ElementsFromList.StripeList;
        }

        private List<Belt> beltsList;
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
        private List<Stripe> stripeList;
        public List<Stripe> StripeList
        {
            get => stripeList;
            set
            {
                if (value == stripeList)
                    return;

                stripeList = value;
                OnPropertyChanged(nameof(StripeList));
            }
        }

        private bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set 
            {
                if (isBusy == value)
                    return;

                isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }
        public bool IsNotBusy => !isBusy;

    }
}

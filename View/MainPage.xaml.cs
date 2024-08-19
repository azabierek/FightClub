using FightClub.ViewModel;

namespace FightClub;

public partial class MainPage : ContentPage
{
	int count = 0;

    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;
    }

}


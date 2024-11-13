using FightClub.ViewModel;

namespace FightClub;

public partial class MainPage : ContentPage
{
    private MainPageViewModel _vm;
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;

        _vm = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Task.Run(async () => await _vm.GetFightersAsync());
    }

}


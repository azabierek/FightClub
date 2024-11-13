using FightClub.ViewModel;

namespace FightClub.View;

public partial class AddFighterPage : ContentPage
{
	public AddFighterPage(AddFighterViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}
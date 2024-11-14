using FightClub.ViewModel;

namespace FightClub.View;

public partial class EditFighterPage : ContentPage
{
	public EditFighterPage(EditFighterViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
using FightClub.Model;
using FightClub.ViewModel;

namespace FightClub.View;

public partial class FighterDetailsPage : ContentPage
{
	public FighterDetailsPage(FighterDetailsViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}
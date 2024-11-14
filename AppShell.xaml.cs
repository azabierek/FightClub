using FightClub.View;

namespace FightClub;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(FighterDetailsPage), typeof(FighterDetailsPage));
		Routing.RegisterRoute(nameof(AddFighterPage), typeof(AddFighterPage));
		Routing.RegisterRoute(nameof(EditFighterPage), typeof(EditFighterPage));
	}
}

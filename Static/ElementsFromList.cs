using FightClub.Model;

namespace FightClub.Static
{
    public static class ElementsFromList
    {
        public static List<string> FilterByList { get; set; }
        public static List<Belt> BeltsList { get; set; }

        static ElementsFromList()
        {
            FilterByList = new List<string>()
            {
                "IMIĘ",
                "NAZWISKO",
                "KSYWA",
                "PAS + WAGA"
            };

            BeltsList = new List<Belt>()
            {
                Belt.Biały,
                Belt.Niebieski,
                Belt.Purpurowy,
                Belt.Brązowy,
                Belt.Czarny,
            };

        }
    }
}

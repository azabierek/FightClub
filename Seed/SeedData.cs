using LiteDB;
using FightClub.Model;

namespace FightClub.Seed
{
    public static class SeedData
    {
        public static void SeedFightersData(LiteDatabase db)
        {
            var fighters = db.GetCollection<Fighter>("Fighter");
            fighters.DeleteAll();
            
            var Ads = new Fighter()
            {
                BirthYear = 1993,
                FirstShowUpYear = 2022,
                Name = "Adrian",
                Surname = "Żabierek",
                Weight = 95,
                Nickname = "Adis",
                Belt = Belt.Niebieski,
                Stripe = Stripe.Zero,
                PhotoSource = "adsq.JPEG"
            };
            var Arek = new Fighter()
            {
                BirthYear = 1993,
                FirstShowUpYear = 2022,
                Name = "Arkadiusz",
                Surname = "Jaroszek",
                Weight = 115,
                Nickname = "Areczek",
                Belt = Belt.Niebieski,
                Stripe = Stripe.Zero,
                PhotoSource = "arrk.JPEG"
            };

            fighters.Insert(Ads);
            fighters.Insert(Arek);

        }
    }
}

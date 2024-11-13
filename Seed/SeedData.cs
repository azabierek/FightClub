using LiteDB;
using FightClub.Model;

namespace FightClub.Seed
{
    public static class SeedData
    {
        public static void SeedFightersData(this LiteDatabase db)
        {
            var fighters = db.GetCollection<Fighter>("Fighter");
            fighters.DeleteAll();

            var notes = db.GetCollection<Note>("Note");
            notes.DeleteAll();
            
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

            var idAds = fighters.Insert(Ads);
            var idArek = fighters.Insert(Arek);

            var noteAds1 = new Note()
            {
                InsertedDate = DateTime.Now.AddDays(-2),
                NoteFromTheCoach = "Do poprawy rozpoczynanie walki.",
                IdFighter = idAds
            };

            var noteAds2 = new Note()
            {
                InsertedDate = DateTime.Now.AddDays(-1),
                NoteFromTheCoach = "Waleczne serce.",
                IdFighter = idAds
            };

            var noteArek = new Note()
            {
                InsertedDate = DateTime.Now.AddHours(-20),
                NoteFromTheCoach = "Maszyna do zabijania, potwór połówki i nie chodzi tu o wóde.",
                IdFighter = idArek
            };

            notes.Insert(noteAds1);
            notes.Insert(noteAds2);
            notes.Insert(noteArek);
        }
    }
}

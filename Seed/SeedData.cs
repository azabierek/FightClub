using LiteDB;
using FightClub.Model;

namespace FightClub.Seed
{
    public static class SeedData
    {
        static Random random = new Random();
        static List<string> names = new List<string> { "Tomasz", "Jan", "Krzysztof", "Marek", "Adam", "Piotr", "Marcin", "Łukasz", "Paweł", "Rafał" };
        static List<string> surnames = new List<string> { "Kowalski", "Nowak", "Wiśniewski", "Wójcik", "Kwiatkowski", "Zieliński", "Kamiński", "Lewandowski", "Szymański", "Dąbrowski" };
        static List<string> nicknames = new List<string> { "Adis", "Storm", "Thunder", "Dragon", "Wolf", "Bear", "Hawk", "Shadow", "Viper", "Phantom" };
        static List<int> weights = new List<int> { 70, 75, 80, 85, 90, 95, 100, 105, 110, 115 };
        public static List<Fighter> GenerateFighters(int count)
        {
            List<Fighter> fighters = new List<Fighter>();

            for (int i = 0; i < count; i++)
            {
                fighters.Add(new Fighter()
                {
                    BirthYear = random.Next(1985, 2000),  
                    FirstShowUpYear = random.Next(2015, 2023), 
                    Name = names[random.Next(names.Count)], 
                    Surname = surnames[random.Next(surnames.Count)], 
                    Weight = weights[random.Next(weights.Count)],
                    Nickname = nicknames[random.Next(nicknames.Count)], 
                    Belt = (Belt)random.Next(0, 4),  
                    Stripe = (Stripe)random.Next(0, 4),
                    PhotoSource = "zt.png"
                });
            }

            return fighters;
        }
        public static void SeedFightersData(this LiteDatabase db)
        {
            var fighters = db.GetCollection<Fighter>("Fighter");
            fighters.DeleteAll();

            var notes = db.GetCollection<Note>("Note");
            notes.DeleteAll();

            //var generatedFighters = GenerateFighters(30);
            //foreach (var fighter in generatedFighters)
            //    fighters.Insert(fighter);

            //var Ads = new Fighter()
            //{
            //    BirthYear = 1993,
            //    FirstShowUpYear = 2022,
            //    Name = "Adrian",
            //    Surname = "Żabierek",
            //    Weight = 98,
            //    Nickname = "Adis",
            //    Belt = Belt.Niebieski,
            //    Stripe = Stripe.Zero,
            //    PhotoSource = "adsq.JPEG"
            //};
            //var Arek = new Fighter()
            //{
            //    BirthYear = 1993,
            //    FirstShowUpYear = 2022,
            //    Name = "Arkadiusz",
            //    Surname = "Jaroszek",
            //    Weight = 120,
            //    Nickname = "Areczek",
            //    Belt = Belt.Niebieski,
            //    Stripe = Stripe.Zero,
            //    PhotoSource = "arrk.JPEG"
            //};

            //var idAds = fighters.Insert(Ads);
            //var idArek = fighters.Insert(Arek);

            //var noteAds1 = new Note()
            //{
            //    InsertedDate = DateTime.Now.AddDays(-2),
            //    NoteFromTheCoach = "Do poprawy rozpoczynanie walki.",
            //    IdFighter = idAds
            //};
            //var noteAds2 = new Note()
            //{
            //    InsertedDate = DateTime.Now.AddDays(-1),
            //    NoteFromTheCoach = "Do poprawy ucieczki z taktarova.",
            //    IdFighter = idAds
            //};
            //var noteArek1 = new Note()
            //{
            //    InsertedDate = DateTime.Now.AddHours(-20),
            //    NoteFromTheCoach = "Dobrze się pokazał na XIX MP, doradzić poprawę siły, lub redukcję wagi.",
            //    IdFighter = idArek
            //};
            //var noteArek2 = new Note()
            //{
            //    InsertedDate = DateTime.Now.AddHours(-20),
            //    NoteFromTheCoach = "Dobre sweepy. Do poprawy motywacja.",
            //    IdFighter = idArek
            //};

            //notes.Insert(noteAds1);
            //notes.Insert(noteAds2);
            //notes.Insert(noteArek1);
            //notes.Insert(noteArek2);
        }
    }
}

using FightClub.Model;
using FightClub.Seed;
using LiteDB;

namespace FightClub.Services
{
    public class FightClubRepository : IFightClubRepository
    {
        private LiteDatabase _db;
        public FightClubRepository() 
        {
            
            _db = new LiteDatabase(@"C:\Users\adrzab\Desktop\FightClub.db");
            
            SeedData.SeedFightersData(_db);
        }
        public Task<Fighter> FindFighterById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Fighter>> FindFightersByName(string nameInput)
        {
            var list = new List<Fighter>();

            await Task.Run(() =>
            {
                if(nameInput is not null)
                {
                    string name = nameInput.Replace(" ", string.Empty)
                            .ToUpper();

                    list = _db.GetCollection<Fighter>("Fighter").
                            Query().
                            Where(x => x.Name.ToUpper().StartsWith(name)).
                            ToList();
                }

            });

            return list;
        }

        public async Task<List<Fighter>> FindFightersByNickname(string nicknameInput)
        {
            var list = new List<Fighter>();

            await Task.Run(() =>
            {
                if (nicknameInput is not null)
                {
                    string nickname = nicknameInput.Replace(" ", string.Empty)
                            .ToUpper();

                    list = _db.GetCollection<Fighter>("Fighter").
                            Query().
                            Where(x => x.Nickname.ToUpper().Contains(nickname)).
                            ToList();
                }

            });

            return list;
        }
        public async Task<List<Fighter>> FindFightersByBeltAndWeight(Belt? belt, int weightFrom, int weightTo)
        {
            var list = new List<Fighter>();

            await Task.Run(() =>
            {
                if (belt != null)
                {
                    list = _db.GetCollection<Fighter>("Fighter").
                        Query().
                        Where(x => x.Weight >= weightFrom && x.Weight <= weightTo && x.Belt == belt).
                        ToList();
                }
                else
                {
                    list = _db.GetCollection<Fighter>("Fighter").
                        Query().
                        Where(x => x.Weight >= weightFrom && x.Weight <= weightTo).
                        ToList();
                }
            });

            return list;
        }

        public async Task<List<Fighter>> FindFightersBySurname(string surnameInput)
        {
            var list = new List<Fighter>();

            await Task.Run(() =>
            {
                if (surnameInput is not null)
                {
                    string surname = surnameInput.Replace(" ", string.Empty)
                            .ToUpper();

                    list = _db.GetCollection<Fighter>("Fighter").
                            Query().
                            Where(x => x.Surname.ToUpper().StartsWith(surname)).
                            ToList();
                }

            });

            return list;
        }

        public Task<IEnumerable<Fighter>> FindFightersByBelt(Belt belt)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Fighter>> GetFighters()
        {
            var returnFighters = new List<Fighter>();

            await Task.Run(() =>
            {
                var fighters = _db.GetCollection<Fighter>("Fighter")
                    .Query()
                    .ToList();

                foreach (var fighter in fighters)
                    returnFighters.Add(fighter);

            });
            return returnFighters;
        }
    }
}

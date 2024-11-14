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
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "FightClub.db");
            _db = new LiteDatabase(dbPath);
            _db.SeedFightersData();
        }

        //CREATE
        public async Task<bool> AddFighter(Fighter fighter, Note firstNote)
        {
            var result = false;
            try
            {
                await Task.Run(() =>
                {
                    var id = _db.GetCollection<Fighter>("Fighter")
                         .Insert(fighter);

                    firstNote.IdFighter = id;

                    var note = _db.GetCollection<Note>("Note")
                        .Insert(firstNote);

                    result = true;
                });
            }
            catch (Exception)
            {
                result = false;
            }
            return result;

        }
        public async Task<bool> AddNoteToFighter(Note note, Fighter fighter)
        {
            var result = false;
            await Task.Run(() =>
            {
                _db.GetCollection<Note>("Note").Insert(note);
                result = true;
            });
            return result;
        }
        //READ
        public Task<Fighter> FindFighterById(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Fighter>> FindFightersByName(string nameInput)
        {
            var list = new List<Fighter>();

            await Task.Run(() =>
            {
                if (nameInput is not null)
                {
                    string name = nameInput.Replace(" ", string.Empty)
                            .ToUpper();

                    list = _db.GetCollection<Fighter>("Fighter").
                            Query().
                            Where(x => x.Name.ToUpper().Contains(name)).
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
                            Where(x => x.Surname.ToUpper().Contains(surname)).
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
        public async Task<IEnumerable<Note>> GetNotesByFighterId(int fighterId)
        {
            var notes = new List<Note>();

            await Task.Run(() =>
            {
                notes = _db.GetCollection<Note>("Note")
                .Query()
                .Where(x => x.IdFighter == fighterId)
                .OrderByDescending(x => x.InsertedDate)
                .ToList();
            });

            return notes;
        }
        //UPDATE
        public async Task<bool> UpdateFighter(Fighter fighter)
        {
            bool result = false;
            await Task.Run(() =>
            {
                _db.GetCollection<Fighter>("Fighter").Update(fighter);
                result = true;
            });
            return result;
        }
        //DELETE
        public async Task<bool> DeleteFighter(Fighter fighter)
        {
            var result = false;
            await Task.Run(() =>
            {
                result = _db.GetCollection<Fighter>("Fighter").Delete(fighter.Id);
            });

            return result;
        }
        public async Task<bool> DeleteNote(int idNote)
        {
            var result = false;
            await Task.Run(() =>
            {
                _db.GetCollection<Note>("Note").Delete(idNote);
                result = true;
            });
            return result;
        }
    }
}

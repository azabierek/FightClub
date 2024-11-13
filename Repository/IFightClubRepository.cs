using FightClub.Model;

namespace FightClub.Services
{
    public interface IFightClubRepository
    {
        Task<Fighter> FindFighterById(int id);
        Task<List<Fighter>> FindFightersByName(string name);
        Task<List<Fighter>> FindFightersBySurname(string surname);
        Task<List<Fighter>> FindFightersByNickname(string nickname);
        Task<List<Fighter>> GetFighters();
        Task<IEnumerable<Fighter>> FindFightersByBelt(Belt belt);
        Task<IEnumerable<Note>> GetNotesByFighterId(int fighterId);
        Task<bool> AddFighter(Fighter fighter, Note note);
        Task<bool> DeleteFighter(Fighter fighter);
        Task<bool> AddNoteToFighter(Note note, Fighter fighter);

    }
}

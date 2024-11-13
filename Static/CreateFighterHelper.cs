using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightClub.Static
{
    public static class CreateFighterHelper
    {
        public static string CapitalizeFirstLetter(this string name) => char.ToUpper(name[0]) + name.Substring(1).ToLower();
        
    }
}

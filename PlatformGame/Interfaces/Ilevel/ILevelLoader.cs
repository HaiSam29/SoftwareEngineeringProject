using PlatformGame.Classes.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Ilevel
{
    // Contract: Level LoadLevel(string levelName)
    // ISP Klein en specifiek; doet alleen level loading
    // DIP LevelFactory zou via deze interface moeten werken
    public interface ILevelLoader
    {
        Level LoadLevel(string levelName);
    }
}

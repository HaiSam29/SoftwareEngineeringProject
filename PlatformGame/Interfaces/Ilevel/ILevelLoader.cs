using PlatformGame.Classes.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Ilevel
{
    public interface ILevelLoader
    {
        Level LoadLevel(string levelName);
    }
}

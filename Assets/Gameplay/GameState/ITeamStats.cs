using System.Collections.Generic;
using Gameplay.Characters;

namespace Gameplay.GameStates
{
    public interface ITeamStats
    {
        public int Score {get;}
        public IReadOnlyList<ICharacterProvider> Characters {get;}
        public ICharacterProvider CurrentServer {get;}

        public void AddScore();
        public void ResetScore();
        public void SwitchServerToNext();
    }
}

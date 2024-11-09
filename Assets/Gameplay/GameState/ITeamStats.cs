using System.Collections.Generic;
using Gameplay.Characters;

namespace Gameplay.GameStates
{
    public interface ITeamStats
    {
        public int Score {get;}
        public IReadOnlyList<ICharacterProvider> Characters {get;}

        public void AddScore();
        public void ResetScore();
    }
}

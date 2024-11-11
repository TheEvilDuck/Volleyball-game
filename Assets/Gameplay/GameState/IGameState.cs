using System.Collections.Generic;
using Common;
using Gameplay.Characters;

namespace Gameplay.GameStates
{
    public interface IGameState: IResatable
    {
        public ITeamStats Team1 {get;}
        public ITeamStats Team2 {get;}
        public ITeamStats CurrentTeam {get;}
        public ITeamStats OtherTeam {get;}
        public IReadOnlyList<ICharacterProvider> AllCharacters {get;}
        public void SwitchTeams();
    }
}

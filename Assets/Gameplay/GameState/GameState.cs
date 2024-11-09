using Common.PlayerInput;
using Gameplay.Characters;
using Gameplay.Maps;

namespace Gameplay.GameStates
{
    public class GameState : IGameState
    {
        private readonly TeamStats[] _currentTeams;
        private int _currentTeam;

        public ITeamStats Team1 => _currentTeams[0];
        public ITeamStats Team2 => _currentTeams[1];
        public ITeamStats CurrentTeam => _currentTeams[_currentTeam];
        public ITeamStats OtherTeam => _currentTeams[SwitchIndex()];

        public GameState(ICharacterFactory characterFactory, Map map, IPlayerInput playerInput)
        {
            _currentTeams = new TeamStats[2];
            _currentTeams[0] = new TeamStats(characterFactory);
            _currentTeams[1] = new TeamStats(characterFactory);

            _currentTeams[0].CreatePlayer(map.Team1ServePosition, playerInput);
            _currentTeams[1].CreatePlayer(map.Team2ServePosition, playerInput);
        }

        public void SwitchTeams() => _currentTeam = SwitchIndex();

        private int SwitchIndex() =>  _currentTeam == 1 ? 0: 1;
    }

}
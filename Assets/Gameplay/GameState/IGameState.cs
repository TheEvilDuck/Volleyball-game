namespace Gameplay.GameStates
{
    public interface IGameState
    {
        public ITeamStats Team1 {get;}
        public ITeamStats Team2 {get;}
        public ITeamStats CurrentTeam {get;}
        public ITeamStats OtherTeam {get;}
        public void SwitchTeams();
    }
}

namespace Gameplay.GameRules
{
    public interface IRulesProvider
    {
        public bool LimitTouches {get;}
        public int MaxTouches {get;}
        public int MaxScoreToWin {get;}
    }
}

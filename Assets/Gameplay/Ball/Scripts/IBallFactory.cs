using Gameplay.StartPositions;

namespace Gameplay.Balls
{
    public interface IBallFactory
    {
        public IBall Get(IStartPosition startPosition);
    }
}

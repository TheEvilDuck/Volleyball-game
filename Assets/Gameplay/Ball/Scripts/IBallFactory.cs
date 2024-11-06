using Gameplay.PositionProviding;

namespace Gameplay.Balls
{
    public interface IBallFactory
    {
        public IBall Get(IPositionProvider startPosition);
    }
}

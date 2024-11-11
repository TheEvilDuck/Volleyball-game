using Gameplay.PositionProviding;

namespace Gameplay.Characters
{
    public interface ICharacterFactory
    {
        public Character Get(IPositionProvider startPosition, float scale = 1);
    }
}

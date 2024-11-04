using Gameplay.StartPositions;

namespace Gameplay.Characters
{
    public interface ICharacterFactory
    {
        public Character Get(IStartPosition startPosition);
    }
}

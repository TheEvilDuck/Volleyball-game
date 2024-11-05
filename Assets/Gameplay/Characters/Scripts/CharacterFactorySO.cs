using Gameplay.StartPositions;
using UnityEngine;

namespace Gameplay.Characters
{
    [CreateAssetMenu(fileName = "Character factory", menuName = "Characters/New character factory SO")]
    public class CharacterFactorySO : ScriptableObject, ICharacterFactory
    {
        [SerializeField] private Character _characterPrefab;
        [SerializeField] private CharacterConfig _characterConfig;

        public Character Get(IStartPosition startPosition)
        {
            Character instance = Instantiate(_characterPrefab, startPosition.Position, startPosition.Rotation);
            instance.Init(_characterConfig);
            return instance;
        }
    }
}

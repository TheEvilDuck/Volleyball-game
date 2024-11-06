using Gameplay.PositionProviding;
using UnityEngine;

namespace Gameplay.Characters
{
    [CreateAssetMenu(fileName = "Character factory", menuName = "Characters/New character factory SO")]
    public class CharacterFactorySO : ScriptableObject, ICharacterFactory
    {
        [SerializeField] private Character _characterPrefab;
        [SerializeField] private CharacterConfig _characterConfig;

        public Character Get(IPositionProvider startPosition)
        {
            Character instance = Instantiate(_characterPrefab, startPosition.Position, startPosition.Rotation);
            instance.Init(_characterConfig);
            return instance;
        }
    }
}

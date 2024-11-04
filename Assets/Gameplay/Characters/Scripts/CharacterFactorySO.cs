using Gameplay.StartPositions;
using UnityEngine;

namespace Gameplay.Characters
{
    [CreateAssetMenu(fileName = "Character factory", menuName = "Characters/New character factory SO")]
    public class CharacterFactorySO : ScriptableObject, ICharacterFactory
    {
        [SerializeField] private Character _characterPrefab;
        [SerializeField, Min(0)] private float _acceleration = 1f;
        [SerializeField, Min(0)] private float _walkSpeed = 5f;

        public Character Get(IStartPosition startPosition)
        {
            Character instance = Instantiate(_characterPrefab, startPosition.Position, startPosition.Rotation);
            instance.Init(_acceleration, _walkSpeed);
            return instance;
        }
    }
}

using Gameplay.PositionProviding;
using UnityEngine;

namespace Gameplay.Characters
{
    [CreateAssetMenu(fileName = "Character factory", menuName = "Characters/New character factory SO")]
    public class CharacterFactorySO : ScriptableObject, ICharacterFactory
    {
        [SerializeField] private Character _characterPrefab;
        [SerializeField] private CharacterConfig _characterConfig;

        public Character Get(IPositionProvider startPosition, float scale = 1)
        {
            Character instance = Instantiate(_characterPrefab, startPosition.Position, startPosition.Rotation);
            Vector3 originalScale = instance.transform.localScale;
            originalScale.x = scale;
            instance.transform.localScale = originalScale;
            instance.Init(_characterConfig, scale);
            return instance;
        }
    }
}

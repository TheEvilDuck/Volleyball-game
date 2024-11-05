using UnityEngine;

namespace Gameplay.Characters
{
    [CreateAssetMenu(fileName = "Character stats", menuName = "Characters/New character stats")]
    public class CharacterConfig : ScriptableObject, ICharacterStats
    {
        [field: SerializeField] public float Acceleration {get; private set;}
        [field: SerializeField] public float WalkSpeed {get; private set;}
        [field: SerializeField] public float MinJumpHeight {get; private set;}
        [field: SerializeField] public float MaxJumpHeight {get; private set;}
        [field: SerializeField] public float ArmsSpeed {get; private set;}
    }
}

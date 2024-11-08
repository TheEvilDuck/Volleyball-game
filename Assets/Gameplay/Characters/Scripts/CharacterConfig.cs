using UnityEngine;

namespace Gameplay.Characters
{
    [CreateAssetMenu(fileName = "Character stats", menuName = "Characters/New character stats")]
    public class CharacterConfig : ScriptableObject, ICharacterStats
    {
        [field: SerializeField, Min(0)] public float Acceleration {get; private set;}
        [field: SerializeField, Min(0)] public float WalkSpeed {get; private set;}
        [field: SerializeField, Min(0)] public float MinJumpHeight {get; private set;}
        [field: SerializeField, Min(0)] public float MaxJumpHeight {get; private set;}
        [field: SerializeField, Min(0)] public float ArmsSpeed {get; private set;}
        [field: SerializeField, Min(0)] public float BendingSpeed {get; private set;}
        [field: SerializeField, Min(0)] public float ArmsMinAngle {get; private set;}
        [field: SerializeField, Range(0, 1)] public float ArmsMinAngleDamping {get; private set;}
        [field: SerializeField, Range(0, 180f)] public float MaxBendAngle {get; private set;}

        private void OnValidate() 
        {
            MinJumpHeight = Mathf.Min(MinJumpHeight, MaxJumpHeight);
            MaxJumpHeight = Mathf.Max(MinJumpHeight, MaxJumpHeight);
        }
    }
}

using UnityEngine;

namespace Gameplay.Balls
{
    [CreateAssetMenu(fileName = "BallConfig", menuName = "Balls/BallConfig")]
    public class BallConfig : ScriptableObject, IBallStats
    {
        [field: SerializeField] public float BallThrowSpeedMultiplier { get; private set; }
    }
}

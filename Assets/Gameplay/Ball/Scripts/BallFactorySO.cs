using Gameplay.StartPositions;
using UnityEngine;

namespace Gameplay.Balls
{
    [CreateAssetMenu(fileName = "Balls factory", menuName = "Balls/New balls factory SO")]
    public class BallFactorySO : ScriptableObject, IBallFactory
    {
        [SerializeField] private Ball _ballPrefab;

        public IBall Get(IStartPosition startPosition)
        {
            Ball instance = Instantiate(_ballPrefab, startPosition.Position, startPosition.Rotation);
            return instance;
        }
    }
}

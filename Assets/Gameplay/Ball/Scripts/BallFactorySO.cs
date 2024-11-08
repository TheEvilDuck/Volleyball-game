using Gameplay.PositionProviding;
using UnityEngine;

namespace Gameplay.Balls
{
    [CreateAssetMenu(fileName = "Balls factory", menuName = "Balls/New balls factory SO")]
    public class BallFactorySO : ScriptableObject, IBallFactory
    {
        [SerializeField] private Ball _ballPrefab;
        [SerializeField] private BallConfig _ballConfig;

        public IBall Get(IPositionProvider startPosition)
        {
            Ball instance = Instantiate(_ballPrefab, startPosition.Position, startPosition.Rotation);
            instance.Init(_ballConfig);
            return instance;
        }
    }
}

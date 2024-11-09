using System;
using System.Collections.Generic;
using Gameplay.BallDetection;
using Gameplay.PositionProviding;
using UnityEngine;

namespace Gameplay.Maps
{
    public class Map : MonoBehaviour, IMapState, IMapInfo
    {
        [SerializeField] private BallCollider _floor;
        [SerializeField] private List<BallCollider> _outZones;
        [SerializeField] private TransformPositionProvider _team1ServePosition;
        [SerializeField] private TransformPositionProvider _team2ServePosition;

        public event Action ballOut;
        public event Action floorHit;

        public IPositionProvider Team1ServePosition => _team1ServePosition;
        public IPositionProvider Team2ServePosition => _team2ServePosition;

        private void OnEnable() 
        {
            _floor.detectionStarted += OnFloorCollision;

            foreach (BallCollider ballCollider in _outZones)
                ballCollider.detectionStarted += OnOutCollision;
        }

        private void OnDisable() 
        {
            _floor.detectionStarted -= OnFloorCollision;

            foreach (BallCollider ballCollider in _outZones)
                ballCollider.detectionStarted -= OnOutCollision;
        }

        private void OnFloorCollision() => floorHit?.Invoke();
        private void OnOutCollision() => ballOut?.Invoke();
    }
}

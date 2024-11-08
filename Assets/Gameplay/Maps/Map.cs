using System;
using System.Collections.Generic;
using Gameplay.BallDetection;
using UnityEngine;

namespace Gameplay.Maps
{
    public class Map : MonoBehaviour, IMapState
    {
        [SerializeField] private BallCollider _floor;
        [SerializeField] private List<BallCollider> _outZones;
        public event Action ballOut;
        public event Action floorHit;

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

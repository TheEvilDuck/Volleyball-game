using System.Collections.Generic;
using UnityEngine;

namespace Common.Tickables
{
    public class TickablesContainer : ITickable
    {
        private readonly List<ITickable> _tickables = new List<ITickable>();

        public void Register(ITickable tickable)
        {
            if (tickable == this)
            {
                Debug.LogWarning("Tickable container must not contain itself, operation will be ignored");
                return;
            }

            if (_tickables.Contains(tickable))
            {
                Debug.LogWarning($"Tickable {tickable} is already registered and will be ignored");
                return;
            }

            _tickables.Add(tickable);
        }

        public void RemoveTickable(ITickable tickable) => _tickables.Remove(tickable);

        public void Tick(float deltaTime)
        {
            for (int i = _tickables.Count - 1; i >= 0; i--)
            {
                if (_tickables[i] != null)
                    _tickables[i].Tick(deltaTime);
                else
                    _tickables.RemoveAt(i);
            }
        }
    }
}

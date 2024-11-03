using System;

namespace Common.Tickables
{
    public struct TickableDelegate : ITickable
    {
        private readonly Action<float> _onTick;

        public TickableDelegate(Action<float> onTick) => _onTick = onTick;

        public void Tick(float deltaTime) => _onTick?.Invoke(deltaTime);
    }

}
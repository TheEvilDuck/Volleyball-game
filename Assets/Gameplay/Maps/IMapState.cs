using System;

namespace Gameplay.Maps
{
    public interface IMapState
    {
        public event Action ballOut;
        public event Action floorHit;
    }
}

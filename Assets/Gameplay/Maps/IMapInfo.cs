using Gameplay.PositionProviding;

namespace Gameplay.Maps
{
    public interface IMapInfo
    {
        public IPositionProvider Team1ServePosition {get;}
        public IPositionProvider Team2ServePosition {get;}
    }
}

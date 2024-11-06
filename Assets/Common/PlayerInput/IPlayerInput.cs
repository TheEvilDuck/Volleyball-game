using System;
using UnityEngine;

namespace Common.PlayerInput
{
    public interface IPlayerInput
    {
        public event Action<float> horizontalInput;
        public event Action jumpPressed;
        public event Action<Vector2> mouseClicked;
        public event Action<Vector2> mousePressHold;
        public event Action<Vector2> mouseRightPressHold;
        public event Action<Vector2> mouseMove;
        public event Action<Vector2> mouseButtonUp;
        public event Action<Vector2> mouseRightButtonUp;
    }
}

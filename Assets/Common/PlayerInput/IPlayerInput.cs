using System;
using UnityEngine;

namespace Common.PlayerInput
{
    public class IPlayerInput
    {
        public event Action<Vector2> horizontalInput;
        public event Action jumpPressed;
        public event Action<Vector2> mouseClicked;
        public event Action<Vector2> mouseMove;
    }
}

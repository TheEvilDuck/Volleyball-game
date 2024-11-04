using System;
using Common.Tickables;
using UnityEngine;

namespace Common.PlayerInput
{
    public class DesktopInput : IPlayerInput, ITickable
    {
        public event Action<float> horizontalInput;
        public event Action jumpPressed;
        public event Action<Vector2> mouseClicked;
        public event Action<Vector2> mouseMove;

        public void Tick(float deltaTime)
        {
            HandleMouse();
            HandleKeyBoard();
        }

        private void HandleMouse()
        {
            mouseMove?.Invoke(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
                mouseClicked?.Invoke(Input.mousePosition);
        }

        private void HandleKeyBoard()
        {
            horizontalInput?.Invoke(Input.GetAxis("Horizontal"));

            if (Input.GetKeyDown(KeyCode.Space))
                jumpPressed?.Invoke();
        }
    }
}

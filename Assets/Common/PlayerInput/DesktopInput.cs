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
        public event Action<Vector2> mousePressHold;
        public event Action<Vector2> mouseRightPressHold;
        public event Action<Vector2> mouseButtonUp;
        public event Action<Vector2> mouseRightButtonUp;

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

            if (Input.GetMouseButton(0))
                mousePressHold?.Invoke(Input.mousePosition);

            if (Input.GetMouseButton(1))
                mouseRightPressHold?.Invoke(Input.mousePosition);

            if (Input.GetMouseButtonUp(0))
                mouseButtonUp?.Invoke(Input.mousePosition);

            if (Input.GetMouseButtonUp(1))
                mouseRightButtonUp?.Invoke(Input.mousePosition);
        }

        private void HandleKeyBoard()
        {
            horizontalInput?.Invoke(Input.GetAxis("Horizontal"));

            if (Input.GetKeyDown(KeyCode.Space))
                jumpPressed?.Invoke();
        }
    }
}

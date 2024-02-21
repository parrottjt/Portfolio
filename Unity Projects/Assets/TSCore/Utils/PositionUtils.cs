using UnityEngine;
using UnityEngine.InputSystem;

namespace TSCore.Utils
{
    public static class PositionUtils
    {
        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePosition = GetMouseWorldPositionWithZAxis(Mouse.current.position.ReadValue(), Camera.main);
            mousePosition.z = 0;
            return mousePosition;
        }

        public static Vector3 GetMouseWorldPositionWithZAxis()
        {
            return GetMouseWorldPositionWithZAxis(Input.mousePosition, Camera.main);
        }

        public static Vector3 GetMouseWorldPositionWithZAxis(Camera worldCamera)
        {
            return GetMouseWorldPositionWithZAxis(Input.mousePosition, worldCamera);
        }

        public static Vector3 GetMouseWorldPositionWithZAxis(Vector3 screenPosition, Camera worldCamera)
        {
            return worldCamera.ScreenToWorldPoint(screenPosition);
        }
    }
}

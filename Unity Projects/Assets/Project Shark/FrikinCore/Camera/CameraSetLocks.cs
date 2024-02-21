using UnityEngine;

namespace FrikinCore.CameraControl
{
    public class CameraSetLocks : MonoBehaviour
    {
        [SerializeField] bool cameraLockX, cameraLockY;

        CameraControlBase cameraControl;

        void Awake()
        {
            cameraControl = FindObjectOfType<CameraControlBase>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameTags.Player.ToString()))
            {
                cameraControl.SetCameraPositionLockXTo(cameraLockX);
                cameraControl.SetCameraPositionLockYTo(cameraLockY);
            }
        }
    }
}

using UnityEngine;

namespace FrikinCore.CameraControl
{
    public abstract class CameraControlBase : MonoBehaviour
    {
        [SerializeField] protected Transform cameraMoveToTransform;

        [SerializeField] protected float cameraFollowSpeed = 1;

        [SerializeField] protected bool cameraPositionLockX, cameraPositionLockY;

        public bool CameraPositionFullLocked => cameraPositionLockX && cameraPositionLockY;

        public void SetBothCameraPositionLocksTo(bool value)
        {
            SetCameraPositionLockXTo(value);
            SetCameraPositionLockYTo(value);
        }

        public void SetCameraPositionLockXTo(bool value) => cameraPositionLockX = value;

        public void SetCameraPositionLockYTo(bool value) => cameraPositionLockY = value;

        public virtual void MoveCameraTo(Transform moveToTransform, float orthographicSize = 20)
        {
            transform.position = moveToTransform.position;
            GameManager.instance.MainCamera.orthographicSize = orthographicSize;
        }
    }
}
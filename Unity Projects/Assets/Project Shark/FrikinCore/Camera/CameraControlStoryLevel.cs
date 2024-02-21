using System;
using FrikinCore.Player;
using TSCore.Time;
using UnityEngine;

namespace FrikinCore.CameraControl
{
    public class CameraControlStoryLevel : CameraControlBase
    {
        float currentFollowPositionX, currentFollowPositionY;
        float cameraPositionX, cameraPositionY;

        void Start()
        {
            cameraMoveToTransform = GameObject.Find("Camera Follow Object").transform;
        }

        void Update()
        {
            if (PlayerManager.instance.Player.IsDead)
            {
                SetBothCameraPositionLocksTo(true);
            }
            else
            {
                if (Math.Abs(GameManager.instance.MainCamera.orthographicSize - 20) > .01f && !CameraPositionFullLocked)
                {
                    GameManager.instance.MainCamera.orthographicSize =
                        Mathf.Lerp(GameManager.instance.MainCamera.orthographicSize, 20, Time.deltaTime);
                }
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            var cameraFollowPosition = cameraMoveToTransform.position;
            currentFollowPositionX = cameraFollowPosition.x;
            currentFollowPositionY = cameraFollowPosition.y;

            var currentCameraPosition = transform.position;
            cameraPositionX = currentCameraPosition.x;
            cameraPositionY = currentCameraPosition.y;

            if (cameraPositionLockX == false)
            {
                cameraPositionX = Mathf.Lerp(cameraPositionX, currentFollowPositionX,
                    TimeManager.Delta * cameraFollowSpeed);
            }

            if (cameraPositionLockY == false)
            {
                cameraPositionY = Mathf.Lerp(cameraPositionY, currentFollowPositionY,
                    TimeManager.Delta * cameraFollowSpeed);
            }

            if (CameraPositionFullLocked) return;
            transform.position = new Vector3(cameraPositionX, cameraPositionY, currentCameraPosition.z);
        }
    }
}
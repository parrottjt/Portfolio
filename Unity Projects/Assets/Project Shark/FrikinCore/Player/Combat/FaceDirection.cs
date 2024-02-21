using FrikinCore.Input;
using TSCore.Utils;
using UnityEngine;

namespace FrikinCore.Player.Combat
{
    public class FaceDirection : AbstractPlayerBehavior
    {
        enum InputControllerType
        {
            Mouse,
            Controller
        }

        InputControllerType controllerType;
        
        float rightVal, upVal;
        Vector2 dir;

        [SerializeField] Transform rotHolder;

        float GetLookAngle => InputManager.instance.ControllerInUse ? Model.RotationAngle : -Model.RotationAngle;

        // Update is called once per frame
        void Update()
        {
            var controllerInUse = InputManager.instance.ControllerInUse;
            controllerType = controllerInUse
                ? InputControllerType.Controller
                : InputControllerType.Mouse;
            Model.LookVector = controllerInUse ? new Vector2(rightVal, -upVal).normalized : dir.normalized;
            Model.IsFacingRight = GetLookAngle is <= 90f and >= -90;

            switch (controllerType)
            {
                case InputControllerType.Mouse:
                    dir = PositionUtils.GetMouseWorldPosition() - transform.position;
                    Model.RotationAngle = Vector2.SignedAngle(Vector2.right, dir);

                    rotHolder.eulerAngles = new Vector3(0, 0, Model.RotationAngle);
                    break;

                case InputControllerType.Controller:
                    //todo: Double check to make sure that controller aiming still works
                    rightVal = InputManager.instance.HorizontalAimAxis();
                    upVal = InputManager.instance.VerticalAimAxis();

                    if (Mathf.Abs(upVal) > 0.5f || Mathf.Abs(rightVal) > 0.5f)
                    {
                        Model.ControllerRightStickReset = false;
                        Model.RotationAngle = Mathf.Atan2(upVal, rightVal) * Mathf.Rad2Deg;
                        rotHolder.rotation = Quaternion.Euler(new Vector3(0, 0, Model.RotationAngle));
                        //newAngle = model.RotationAngle;
                    }
                    else
                    {
                        Model.ControllerRightStickReset = true;
                        rotHolder.rotation = Quaternion.Euler(0, 0, Model.RotationAngle);
                    }

                    break;
            }
        }
    }
}
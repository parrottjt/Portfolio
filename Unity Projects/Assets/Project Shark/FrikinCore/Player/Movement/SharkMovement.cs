using FrikinCore.Input;
using UnityEngine;

namespace FrikinCore.Player.Movement
{
    public class SharkMovement : AbstractPlayerBehavior
    {
        // Update is called once per frame
        void Update()
        {
            var moveHorizontal = InputManager.instance.HorizontalMoveAxis();
            var moveVertical = InputManager.instance.VerticalMoveAxis();

            Model.Animator.SetBool(PlayerDataModel.IsMovingHash, Model.IsMoving);

            SinglePlayerMovement(moveHorizontal, moveVertical);
        }

        void FixedUpdate()
        {
            Model.Rigidbody.AddRelativeForce(Model.MovementVector * Model.Speed);
        }

        void SinglePlayerMovement(float moveHorizontal, float moveVertical)
        {
            if (Model.IsStunned || Model.IsDead)
            {
                Model.MovementVector = Vector2.zero;
            }
            else
            {
                var movementVector = Model.MovementVector;
                movementVector.x = Mathf.Abs(moveHorizontal) > 0 ? moveHorizontal : 0f;
                movementVector.y = Mathf.Abs(moveVertical) > 0 ? moveVertical : 0f;
                Model.MovementVector = movementVector;
            }

            Model.Animator.SetFloat(PlayerDataModel.XAxisHash, Model.MovementVector.x);
            Model.Animator.SetFloat(PlayerDataModel.YAxisHash, Model.MovementVector.y);
            Model.Animator.SetFloat(PlayerDataModel.DirectionIndexFloatHash, Model.DirectionIndex);
        }
    }
}